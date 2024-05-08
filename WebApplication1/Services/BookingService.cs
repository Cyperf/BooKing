using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Services
{
	public class BookingService : Repository<Booking>
	{
		public BookingService()
		{
			_tableName = "Booking";
		}
		protected override Func<Booking, string> _fromItemToString { get; } = (booking) => 
		{ 
			return $"'{booking.Dato.Year + "-" + booking.Dato.Month + "-" + booking.Dato.Day}', {booking.TidFra}, {booking.TidTil}, '{booking.Gruppemedlem}', {booking.LokaleId}, {booking.SkoleId}, {booking.BookingType.Id}"; 
		};

		protected override Func<SqlDataReader, Booking> _fromReaderToItem { get; } = (reader) =>
		{
			Booking booking = new Booking(reader.GetInt32(0), DateOnly.FromDateTime(reader.GetDateTime(1)), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), null); // reader.GetInt32(7)
			return booking;
			//return $"{booking.Id}, {booking.Dato}, {booking.TidFra}, {booking.TidTil}, {booking.Gruppemedlem}, {booking.LokaleId}, {booking.SkoleId}, {booking.BookingType.Id}";
		};
		/// <summary>
		/// Use TryCreate instead
		/// </summary>
		/// <param name="newItem"></param>
        public override void Create(Booking newItem)
        {}
        /// <summary>
        /// check if a booking is valid, before createing it.
        /// </summary>
        /// <returns></returns>
        public bool TryCreate(Booking booking)
		{
			// check if the booking is valid
			// check the booking time makes sense
			int midnight = 1 * 60 * 24;
			if (!(booking.TidFra < booking.TidTil && booking.TidFra >= 0 && booking.TidTil <= midnight))
				return false;
            // check the room is available in the time frame (no one else has booked it)
            var relevantBookings = ReadAll($"Dato='{booking.Dato.Year + "-" + booking.Dato.Month + "-" + booking.Dato.Day}' AND LokaleId = {booking.LokaleId} AND SkoleId={booking.SkoleId}");
			int bookingsInInterval = 0;
			foreach (var otherBooking in relevantBookings)
				if ((booking.TidFra >= otherBooking.TidFra && booking.TidFra <= otherBooking.TidTil) ||
					(booking.TidTil > otherBooking.TidFra && booking.TidTil < otherBooking.TidTil))
					bookingsInInterval++;
			// make sure the booking count is less that the room allows
			if (bookingsInInterval >= new LokaleService().Read(booking.LokaleId).MaxGrupperAdGangen)
				return false;

            // check the user can place a booking in the time frame?
            base.Create(booking);
            return true;
        }
        public override void Update(Booking item)
        {
            throw new NotImplementedException();
        }
	}
}

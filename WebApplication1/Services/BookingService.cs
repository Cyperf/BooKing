using Microsoft.Data.SqlClient;
using WebApplication1.Models;

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
			return $"{booking.Id}, {booking.Dato}, {booking.TidFra}, {booking.TidTil}, {booking.Gruppemedlem}, {booking.LokaleId}, {booking.SkoleId}, {booking.BookingType.Id}"; 
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
			// check the room is available in the time frame
			// 
            base.Create(booking);
			return true;
        }

		public override void Update(Booking item)
		{
			throw new NotImplementedException();
		}
	}
}

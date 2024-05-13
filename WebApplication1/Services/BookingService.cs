﻿using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Services
{
	public class BookingService : Repository<Booking>
	{
		public const int EarliestAllowedBooking = 60 * 6; // 06:00
		public const int LatestAllowedBooking = 60 * 20; // 20:00
		public const int MaxBookingLength = 60 * 2; // you can max book a room for 2 hours (120 minutes)
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
			Booking booking = new Booking(reader.GetInt32(0), DateOnly.FromDateTime(reader.GetDateTime(1)), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), new BookingTypeRepository().Read(reader.GetInt32(7))); // reader.GetInt32(7)
			return booking;
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
        public string TryCreate(Booking booking)
		{
			// check if the booking is valid
			// check the booking time makes sense
			if (booking.TidFra >= booking.TidTil || booking.TidFra < EarliestAllowedBooking || booking.TidTil > LatestAllowedBooking)
				return $"Vær sød at opret en booking mellem {FromMinutesToTime(EarliestAllowedBooking)} og {FromMinutesToTime(LatestAllowedBooking)}";
			// check length of booking
			if (booking.TidTil - booking.TidFra > MaxBookingLength)
				return $"Din booking er lang, den må maksimalt være {MaxBookingLength}";
            // check the room is available in the time frame (no one else has booked it)
            {
                var relevantBookings = ReadAll($"Dato='{booking.Dato.Year + "-" + booking.Dato.Month + "-" + booking.Dato.Day}' AND LokaleId = {booking.LokaleId} AND SkoleId={booking.SkoleId}");
				int[] bookingsInInterval = new int[booking.TidTil - booking.TidFra];
				foreach (var otherBooking in relevantBookings)
					for (int i = (otherBooking.TidFra > booking.TidFra ? otherBooking.TidFra : booking.TidFra);
					i < (otherBooking.TidTil < booking.TidTil ? otherBooking.TidTil : booking.TidTil); i++)
						bookingsInInterval[i - booking.TidFra]++;
                // make sure the booking count is less that the room allows
                if (bookingsInInterval.Max() >= new LokaleService().Read(booking.LokaleId).MaxGrupperAdGangen)
                    return $"Lokalet er desværre fuld optaget i det interval du har indsat";
            }

			// check if the user can place a booking in the time frame (if they are not an admin)
			{
				Bruger bruger = new BrugerService().Read(booking.Gruppemedlem);
                if (bruger.Rolle.RolleNavn != "admin")
                {
					foreach (var otherBooking in ReadAll($"BrugerEmail={bruger.Email}"))
					{
						if ((booking.TidFra >= otherBooking.TidFra && booking.TidFra <= otherBooking.TidTil) ||
							(booking.TidTil > otherBooking.TidFra && booking.TidTil < otherBooking.TidTil))
							return "Du har allerede en booking i denne tidsramme";

                    }
                }
            }
            base.Create(booking);
			return CreateAcceptMessage();
        }
		public static string CreateAcceptMessage()
		{
			return "Oprettede succesfult bookningen";
        }
        public string FromMinutesToTime(int minutes)
        {
            int hours = minutes / 60;
            return $"{hours,2}:{minutes % 60,2}";
        }

        public override void Update(Booking item)
        {
            throw new NotImplementedException();
        }
	}
}

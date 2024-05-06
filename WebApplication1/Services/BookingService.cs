using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
	public class BookingService : Repository<Booking>
	{
		protected override Func<Booking, string> _fromItemToString { get; } = (booking) => 
		{ 
			return $"{booking.Id}, {booking.Dato}, {booking.TidFra}, {booking.TidTil}, {booking.Gruppemedlem}, {booking.LokaleId}, {booking.SkoleId}, {booking.BookingType.Id}"; 
		};

		protected override Func<SqlDataReader, Booking> _fromReaderToItem { get; } = (reader) =>
		{
			Booking booking = new Booking(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), null); // reader.GetInt32(7)
			return booking;
			//return $"{booking.Id}, {booking.Dato}, {booking.TidFra}, {booking.TidTil}, {booking.Gruppemedlem}, {booking.LokaleId}, {booking.SkoleId}, {booking.BookingType.Id}";
		};

		public override void Update(Booking item)
		{
			throw new NotImplementedException();
		}
	}
}

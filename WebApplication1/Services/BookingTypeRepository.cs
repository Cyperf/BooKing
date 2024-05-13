using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
	public class BookingTypeRepository : Repository<BookingType>
	{
		public BookingTypeRepository()
		{
			_tableName = "BookingType";
		}
		public static BookingTypeRepository Instance = new BookingTypeRepository();
		protected override Func<BookingType, string> _fromItemToString { get; } = (bookingType) =>
		{
			return $"'{bookingType.Type}'";
		};

		protected override Func<SqlDataReader, BookingType> _fromReaderToItem { get; } = reader =>
		{
			return new BookingType(reader.GetString(1), reader.GetInt32(0));
		};

		public override void Update(BookingType item)
		{
			throw new NotImplementedException();
		}
	}
}

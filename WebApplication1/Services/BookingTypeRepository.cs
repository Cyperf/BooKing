using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.SQL;

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
			return $"'{bookingType.Type.ToLower()}'";
		};

		protected override Func<SqlDataReader, BookingType> _fromReaderToItem { get; } = reader =>
		{
			return new BookingType(reader.GetString(1), reader.GetInt32(0));
		};

		public int GetIdByName(string typeName)
		{
			int id = -1;
			AdoNet.ExecuteQuery($"SELECT Id FROM BookingType WHERE TypeNavn='{typeName.ToLower()}'", reader =>
			{
				if (!reader.Read())
					return;
				id = reader.GetInt32(0);
			});

            return id;
		}

		public override void Update(BookingType item)
		{
			throw new NotImplementedException();
		}
	}
}

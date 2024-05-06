namespace WebApplication1.Services
{
	using Microsoft.Data.SqlClient;
	using System;
	using WebApplication1.Models;
	public class SkoleService : Repository<Skole>
	{
		public SkoleService() 
		{
			_tableName = "Skole";
		}

		protected override Func<Skole, string> _fromItemToString { get; } = (skole) =>
		{
			return $"{skole.Id}, {skole.Location}";
		};

		protected override Func<SqlDataReader, Skole> _fromReaderToItem { get; } = (reader) =>
		{
			return new Skole(reader.GetInt32(0), reader.GetString(1));
		};

		public override void Update(Skole item)
		{
		}
        private void AddMockData()
        {
            Create(new Skole(1, "Roskilde"));
            Create(new Skole(2, "Køge"));
            Create(new Skole(3, "Næstved"));


        }
    }
}

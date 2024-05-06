using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Pages.OurPages;
using WebApplication1.SQL;

namespace WebApplication1.Services
{
    public class LokaleService : Repository<Lokale>
    {

        public LokaleService()
        {
            _tableName = "Lokale";
        }

        public override void Update(Lokale item)
        {
            throw new NotImplementedException();
        }

        protected override Func<Lokale, string> _fromItemToString { get; } = (lokale) =>
        {
            return $"{lokale.Id}, {lokale.SkoleId}, {lokale.HarSmartBoard}";
        };

		protected override Func<SqlDataReader, Lokale> _fromReaderToItem { get; } = (reader) =>
		{
			return new Lokale(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetBoolean(3));
		};

		//public void Create(Lokale lokale)
		//      {
		//          _lokaler.Create(lokale);
		//      }
		//      public Lokale? Read(Lokale lokale)
		//      {
		//          return _lokaler.Read(lokale.Id);
		//      }
		//      public Lokale? Read(int lokaleId)
		//      {
		//          return _lokaler.Read(lokaleId);
		//      }
		//      public void Update(Lokale lokale)
		//      {
		//          _lokaler.Update(lokale);
		//      }
		//      public void Delete(Lokale lokale)
		//      {
		//          if (lokale != null)
		//          {

		//          }
		//          else throw new NotImplementedException();
		//      }


	}
    
}



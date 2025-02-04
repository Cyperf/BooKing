﻿// Jeppe
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

        protected override Func<Lokale, string> _fromItemToString { get; } = (lokale) =>
        {
            return $"{lokale.Id}, {lokale.SkoleId}, {lokale.MaxGrupperAdGangen}, {(lokale.HarSmartBoard ? 1 : 0)}";
        };

		protected override Func<SqlDataReader, Lokale> _fromReaderToItem { get; } = (reader) =>
		{
			return new Lokale(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetByte(3) == 1 ? true : false);
		};
        public override void Update(Lokale item)
        {
			AdoNet.ExecuteNonQuery($"UPDATE {_tableName} SET MaxGrupperAfGangen={item.MaxGrupperAdGangen}, HarSmartboard={(item.HarSmartBoard ? 1 : 0)} WHERE Id={item.Id} AND SkoleId={item.SkoleId}");
        }
        public void Delete(int id, int skoleId)
        {
            AdoNet.ExecuteNonQuery($"DELETE FROM {_tableName} WHERE id={id} AND SkoleId={skoleId}");
        }


        public override Lokale? Read(int id)
        {
			return Read(id, LoginManager.LoggedInUser.SkoleId);
        }

        public Lokale Read(int lokaleId, int skoleId)
        {
            Lokale? item = default;
            AdoNet.ExecuteQuery($"SELECT * FROM {_tableName} WHERE Id={lokaleId} AND SkoleId={skoleId}", (reader) =>
            {
                if (!reader.Read())
                    return;
                item = _fromReaderToItem(reader);
            });
            return item;
		}

	}
    
}



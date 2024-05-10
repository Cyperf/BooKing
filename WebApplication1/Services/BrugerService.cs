using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.SQL;

namespace WebApplication1.Services
{
    public class BrugerService : Repository<Bruger>
    {
        public BrugerService()
        {
            _tableName = "Bruger";
        }
        protected override Func<Bruger, string> _fromItemToString { get; } = bruger =>
        {
            return $"'{bruger.Navn}', '{bruger.Email}', '{LoginManager.HashPassword(bruger.Email, bruger.Kodeord)}', {bruger.brugerId}, {bruger.SkoleId}, '{bruger.SletningsDato.Year+"-"+ bruger.SletningsDato.Month+"-"+ bruger.SletningsDato.Day}'";
        };

        protected override Func<SqlDataReader, Bruger> _fromReaderToItem { get; } = reader =>
        {                                                                                    // not null, since it is a foreign key :)
            return new Bruger(reader.GetString(0), reader.GetString(1), reader.GetString(2), new BrugerRolleService().Read(reader.GetInt32(3)), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5)));
        };

        public Bruger Read(string email)
        {
            Bruger? item = default;
            AdoNet.ExecuteQuery($"SELECT * FROM {_tableName} WHERE Email='{email}'", (reader) =>
            {
                if (!reader.Read())
                    return;
                item = _fromReaderToItem(reader);
            });
            return item;
        }
        /// <summary>
        /// Do not use, the user does not have an "Id" field
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Bruger? Read(int id)
        {
            return null;
        }

        public override void Update(Bruger item)
        {
            AdoNet.ExecuteNonQuery($"UPDATE {_tableName} SET Name = '{item.Navn}', Kode = '{item.Kodeord}', WHERE Email = '{item.Email}'");
        }
    }
}

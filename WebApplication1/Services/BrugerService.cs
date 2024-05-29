// Jeppe
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
            return $"'{bruger.Navn}', '{bruger.Email}', '{LoginManager.HashPassword(bruger.Email, bruger.Kodeord)}', {bruger.Rolle.Id}, {bruger.SkoleId}, '{bruger.SletningsDato.Year+"-"+ bruger.SletningsDato.Month+"-"+ bruger.SletningsDato.Day}'";
        };

        protected override Func<SqlDataReader, Bruger> _fromReaderToItem { get; } = reader =>
        {
            System.Diagnostics.Debug.WriteLine( new Bruger(reader.GetString(0), reader.GetString(1), reader.GetString(2), new BrugerRolleService().Read(reader.GetInt32(3)), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5))).Rolle.RolleNavn);
            // not null, since it is a foreign key :)
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

        public void DeleteByEmail(string email)
        {
            // delete all the persons bookings, since we use a foreign key
            var bookingService = new BookingService();
            foreach (var booking in bookingService.ReadAll($"BrugerEmail='{email}'"))
            {
                bookingService.Delete(booking.Id);
            }
            AdoNet.ExecuteNonQuery($"DELETE FROM {_tableName} WHERE Email='{email}'");
        }
        public override void Update(Bruger item)
        {
            AdoNet.ExecuteNonQuery($"UPDATE {_tableName} " +
                $"SET Kode = '{LoginManager.HashPassword(item.Email, item.Kodeord)}'" +
                $" WHERE Email = '{item.Email}';");
        }

        public void AdminUpdate(Bruger item)
        {
            AdoNet.ExecuteNonQuery($"UPDATE {_tableName} " +
                $"SET Kode = '{LoginManager.HashPassword(item.Email, item.Kodeord)}'," +
                $" Name = '{item.Navn}'," +
                $" Email = '{item.Email}'," +
                $" Rolle = '{item.Rolle.Id}'," +
                $" SkoleId = '{item.SkoleId}'," +
                $" SletningsDato = '{item.SletningsDato}'" +
                $" WHERE Email = '{item.Email}';");

        }

        public void CleanOldAccounts()
        {
            DateOnly currentDay = DateOnly.FromDateTime(DateTime.Now);
            foreach (var booking in ReadAll($"SletningsDato<'{currentDay.Year + "-" + currentDay.Month + "-" + currentDay.Day}'"))
            {
                DeleteByEmail(booking.Email);
            }
        }
    }
}

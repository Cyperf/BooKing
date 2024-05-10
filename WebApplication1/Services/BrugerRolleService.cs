using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    // HVIS MULIGT FJERN DEN HER MEN JEG GAV OP
    public class BrugerRolleService : Repository<BrugerRolle>
    {
        public BrugerRolleService()
        {
            _tableName = "BrugerRolle";
        }
        protected override Func<BrugerRolle, string> _fromItemToString { get; } = brugerRolle => 
        {
            return $"'{brugerRolle.RolleNavn.ToLower()}', {(brugerRolle.DagesVarselIndenOverskrivelse != null ? brugerRolle.DagesVarselIndenOverskrivelse.Value : "null")}";
        };


        protected override Func<SqlDataReader, BrugerRolle> _fromReaderToItem { get; } = reader =>
        {
            return new BrugerRolle(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2));
        };

        //protected override Func<SqlDataReader, Bruger> _fromReaderToItem { get; } = reader =>
        //{                                                                                    // not null, since it is a foreign key :)
        //    return new Bruger(reader.GetString(0), reader.GetString(1), reader.GetString(2), new BrugerRolleService().Read(reader.GetInt32(3)), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5)));
        //};

        public override void Update(BrugerRolle item)
        {
            throw new NotImplementedException();
        }
    }
}

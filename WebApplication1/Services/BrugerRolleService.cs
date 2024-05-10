using Microsoft.Data.SqlClient;
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
            return new BrugerRolle(reader.GetInt32(0), reader.GetString(1), reader.IsDBNull(2) ? null : reader.GetInt32(2));
        };

        public override void Update(BrugerRolle item)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class BrugerService : Repository<Bruger>
    {
        public BrugerService() 
        {
            _tableName = "Bruger";
        }

        protected override Func<Bruger, string> _fromItemToString => throw new NotImplementedException();

        protected override Func<SqlDataReader, Bruger> _fromReaderToItem => throw new NotImplementedException();

        public override void Update(Bruger item)
        {
            throw new NotImplementedException();
        }
    }
}

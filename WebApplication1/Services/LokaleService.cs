using WebApplication1.Models;
using WebApplication1.Pages.OurPages;
using WebApplication1.SQL;

namespace WebApplication1.Services
{
    public class LokaleService
    {

        Repository<Lokale?> _lokaler;


        public void Create(Lokale lokale)
        {
            _lokaler.Create(lokale);
        }
        public Lokale? Read(Lokale lokale)
        {
            return _lokaler.Read(lokale.Id);
        }
        public Lokale? Read(int lokaleId)
        {
            return _lokaler.Read(lokaleId);
        }
        public void Update(Lokale lokale)
        {
            _lokaler.Update(lokale);
        }
        public void Delete(Lokale lokale)
        {
            if (lokale != null)
            {

            }
            else throw new NotImplementedException();
        }


    }
}


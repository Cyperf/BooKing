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

        private void AddMockData()
        {
            Create(new Lokale(101, 1, 2, true));
            Create(new Lokale(102, 1, 3, true));
            Create(new Lokale(103, 1, 4, false));
            Create(new Lokale(201, 1, 2, true));
            Create(new Lokale(202, 1, 2, true));
            Create(new Lokale(203, 1, 1, false));
            Create(new Lokale(204, 1, 1, false));

        }
    }
}


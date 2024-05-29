// Frederik
namespace WebApplication1.Models
{
    public class Lokale
    {
        public int Id { get; set; }
        public int SkoleId { get; set; }
        public int MaxGrupperAdGangen { get; set; }
        public bool HarSmartBoard { get; set; }

        public int Etage { get; set; }

        public Lokale() { Id = 999;SkoleId = 999;MaxGrupperAdGangen = 999; HarSmartBoard = true; }
        public Lokale(int id, int skoleid, int maxgrupperadgangen, bool harsmartboard)
        {
            Id = id;
            SkoleId = skoleid;
            MaxGrupperAdGangen = maxgrupperadgangen;
            HarSmartBoard = harsmartboard;
            Etage = id / 100; // id is saved as floor (one digit) rest of id (two digits)
        }
        public override string ToString()
        {
            return $"Id: {Id}, SkoleId: {SkoleId}, Maximum gruppe kapacitet {MaxGrupperAdGangen}, Smartboard? {HarSmartBoard}";
        }
    }
}

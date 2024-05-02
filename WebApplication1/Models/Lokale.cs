namespace WebApplication1.Models
{
    public class Lokale
    {
        public int Id { get; set; }
        public int SkoleId { get; set; }
        public int MaxGrupperAdGangen { get; set; }
        public bool HarSmartBoard { get; set; }
    

    public Lokale(int id, int skoleid, int maxgrupperadgangen, bool harsmartboard)
    {

        Id = id;
        SkoleId = skoleid;
        MaxGrupperAdGangen = maxgrupperadgangen;
        HarSmartBoard = harsmartboard;
    }
        public override string ToString()
        {
            return $"Id: {Id}, SkoleId: {SkoleId}, Maximum gruppe kapacitet {MaxGrupperAdGangen}, Smartboard? {HarSmartBoard}";
        }
    }
}

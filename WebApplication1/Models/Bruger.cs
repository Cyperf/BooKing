namespace WebApplication1.Models
{
    public class Bruger
    {
        public string Navn { get; set; }
        public string Email { get; set; }
        public string Kodeord { get; set; }
        public BrugerRolle BrugerRolle { get; set; }
        public int SkoleId { get; set; }

        public Bruger()
        {

        }
        public bool Ændrekodeord(string kode)
        {
            return true;
        }
    }
}

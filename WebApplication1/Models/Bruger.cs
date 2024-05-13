namespace WebApplication1.Models
{
    public class Bruger
    {
        public string Navn { get; set; }
        public string Email { get; set; }
        public string Kodeord { get; set; }
        public BrugerRolle Rolle { get; set; }
        public int SkoleId { get; set; }
        public DateOnly SletningsDato;
        public int brugerId { get; set; }
        

        public Bruger()
        {

        }
        public Bruger(string navn, string email, string kodeord, BrugerRolle rolle, int skoleId, DateOnly sletningsDato)
		{
            Navn = navn;
            Email = email;
            Kodeord = kodeord;
            Rolle = rolle;
            brugerId = rolle.Id;
            SkoleId = skoleId;
			SletningsDato = sletningsDato;
        }
        public Bruger(string navn, string email, string kodeord, int brugerId, int skoleId, DateOnly sletningsDato)
        {
            Navn = navn;
            Email = email;
            Kodeord = kodeord;
            Rolle = new WebApplication1.Services.BrugerRolleService().Read(brugerId);
            brugerId = Rolle.Id;
            SkoleId = skoleId;
            SletningsDato = sletningsDato;
        }
        public bool Ændrekodeord(string kode)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Navn: {this.Navn}. Skole {this.SkoleId}. Email {this.Email}. BrugerId {this.brugerId}. Kodeord {this.Kodeord}. Sletningsdato {this.SletningsDato}";
        }
    }
}

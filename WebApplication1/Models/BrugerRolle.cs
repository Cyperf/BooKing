// Jeppe
namespace WebApplication1.Models
{
    public class BrugerRolle
    {
        public int Id { get; set; }
        public string RolleNavn {  get; set; }
        public int? DagesVarselIndenOverskrivelse {  get; set; }
		public BrugerRolle()
		{
		}
		public BrugerRolle(string rolleNavn, int? dagesVarselIndenOverskrivelse)
        {
			RolleNavn = rolleNavn;
            DagesVarselIndenOverskrivelse = dagesVarselIndenOverskrivelse;
		}
        public BrugerRolle(int id, string rolleNavn, int? dagesVarselIndenOverskrivelse)
        {
            Id = id;
            RolleNavn = rolleNavn;
            DagesVarselIndenOverskrivelse = dagesVarselIndenOverskrivelse;
        }
    }
}

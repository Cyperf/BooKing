namespace WebApplication1.Models
{
    public class BrugerRolle
    {
        public int Id { get; set; }
        public string BrugerType {  get; set; }
        public BrugerRolle(string brugertype)
        {
           BrugerType = brugertype;
        }
    }
}

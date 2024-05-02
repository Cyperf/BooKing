namespace WebApplication1.Models
{
    public class Skole
    {
        public int Id { get; set; }
        public string Location { get; set; }

        public Skole(int id, string location)
        {
            Id = id;
            Location = location;
        }


        public override string ToString()
        {
            return $"Id: {Id}, Location: {Location}";
        }
    }
}
// Frederik
namespace WebApplication1.Models
{
    public class BookingType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public BookingType()
        {

        }
        public BookingType(string type, int id = -1)
        {
            Id = id;
            Type = type;
        }
    }
}

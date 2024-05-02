namespace WebApplication1.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public int TidFra {  get; set; }
        public int TidTil {  get; set; }
        public int GruppemedlemId { get; set; }
        public int LokaleId { get; set; }
        public BookingType BookingType { get; set; }

        public Booking()
        {
            //Automatisk Id
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

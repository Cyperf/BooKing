namespace WebApplication1.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public int TidFra {  get; set; }
        public int TidTil {  get; set; }
        public string Gruppemedlem { get; set; }
        public int LokaleId { get; set; }
        public int SkoleId { get; set; }
        public BookingType BookingType { get; set; }

        public Booking()
        {

        }
		public Booking(int id, DateTime dato, int tidFra, int tidTil, string gruppeMedlem, int lokaleId, int skoleId, BookingType bookingType)
		{
            Id = id;
            Dato = dato;
            TidFra = tidFra;
            TidTil = tidTil;
            Gruppemedlem = gruppeMedlem;
            LokaleId = lokaleId;
            SkoleId = skoleId;
            this.BookingType = bookingType;
		}

		public override string ToString()
        {
            return base.ToString();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class BookLokaleModel : PageModel
    {
        [BindProperty] public int NewBookingStartHour { get; set; }
        [BindProperty] public int NewBookingStartMinute { get; set; }
        [BindProperty] public int NewBookingEndHour { get; set; } = 2;
        [BindProperty] public int NewBookingEndMinute { get; set; }

        public string? FejlMeddelse = null;

        public static string Skole { get; private set; }
        public static int _lokaleId = -1, _skoleId = -1;
        public static DateOnly _date;
        public IActionResult OnGet(DateOnly? date = null, int lokaleId = -1, int skoleId = -1)
        {
            // check for invalid data
            if (date == null || lokaleId == -1 || skoleId == -1)
                return RedirectToPage("Booking");
            _lokaleId = lokaleId;
            _skoleId = skoleId;
            _date = date.Value;
            Skole = new SkoleService().Read(skoleId).Location;
            return Page();
        }
        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine(NewBookingStartHour + ":" + NewBookingStartMinute + " to " + NewBookingEndHour + ":" + NewBookingEndMinute);
            int start = NewBookingStartHour * 60 + NewBookingStartMinute;
            int end = start + NewBookingEndHour * 60 + NewBookingEndMinute;
            if (new BookingService().TryCreate(new Booking(-1, _date, start, end, LoginManager.LoggedInUser.Email, _lokaleId, _skoleId, new BookingTypeRepository().Read(1))))
                FejlMeddelse = "Oprettede succesfult bookningen";
            else
                FejlMeddelse = "Bookningen kunne desvære ikke oprettes.";
            return Page();
        }

        public List<TidsInterval> GetRoomAvailability(DateOnly date)
        {
            List<TidsInterval> availability = new List<TidsInterval>();
            // first we need to find every minute the room is available
            bool[] minutesAvailable = new bool[1 * 60 * 24];
            List<Booking> bookings = new BookingService().ReadAll($"Dato='{date.Year + "-" + date.Month + "-" + date.Day}' AND SkoleId={_skoleId}").ToList();
            foreach (var booking in bookings.Where(book => book.LokaleId == _lokaleId))
                for (int i = booking.TidFra; i < booking.TidTil; i++)
                    minutesAvailable[i] = true;
            // we then need to convert them to an interval of minutes
            bool available = minutesAvailable[0];
            int startInterval = 0;
            for (int i = 0; i < minutesAvailable.Length-1; i++)
            {
                if (minutesAvailable[i] != available)
                {
                    int endInterval = i - 1;
                    availability.Add(new TidsInterval(startInterval, endInterval, !available)); //minutesAvailable gives "false" when it is available
                    startInterval = i;
                    available = !available;
                }
            }
            // add final interval
            {
                int endInterval = minutesAvailable.Length-1;
                availability.Add(new TidsInterval(startInterval, endInterval, !available));
            }
            return availability;
        }

        public string FromMinutesToTime(int minutes)
        {
            int hours = minutes / 60;
            return $"{hours,2}:{minutes % 60, 2}";
        }

        public class TidsInterval
        {
            public int Start { get; }
            public int End { get; }
            public bool Available { get; }
            public TidsInterval(int start, int end, bool available)
            {
                Start = start;
                End = end;
                Available = available;
            }

        }
    }

    public class TidsInterval
    {

    }
}

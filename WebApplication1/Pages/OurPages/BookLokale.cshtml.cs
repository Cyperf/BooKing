using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Security.Claims;

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
        public IActionResult OnGet(int year = 1, int month = 1, int day = 1, int lokaleId = -1, int skoleId = -1)
        {
            System.Diagnostics.Debug.WriteLine($"y {year} m {month} d {day}");
            // check for invalid data
            if (lokaleId == -1 || skoleId == -1)
                return RedirectToPage("Booking");
            _lokaleId = lokaleId;
            _skoleId = skoleId;
            _date = new DateOnly(year, month, day);
            Skole = new SkoleService().Read(skoleId).Location;
            return Page();
        }
        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine(NewBookingStartHour + ":" + NewBookingStartMinute + " to " + NewBookingEndHour + ":" + NewBookingEndMinute);
            int start = NewBookingStartHour * 60 + NewBookingStartMinute;
            int end = start + NewBookingEndHour * 60 + NewBookingEndMinute;
            Booking newBooking = new Booking(-1, _date, start, end, LoginManager.LoggedInUser.Email, _lokaleId, _skoleId, new BookingTypeRepository().Read(1));
            FejlMeddelse = new BookingService().TryCreate(newBooking);
            return Page();
        }

        

        public List<TidsInterval> GetRoomAvailability(DateOnly date)
        {
            List<TidsInterval> availability = new List<TidsInterval>();
            // first we need to find every minute the room is available
            int earliestAllowedBooking = BookingService.EarliestAllowedBooking;
            int latestAllowedBooking = BookingService.LatestAllowedBooking;
            int[] minutesAvailable = new int[latestAllowedBooking - earliestAllowedBooking];
            List<Booking> bookings = new BookingService().ReadAll($"Dato='{date.Year + "-" + date.Month + "-" + date.Day}' AND LokaleId = {_lokaleId} AND SkoleId={_skoleId}").ToList();
            foreach (var booking in bookings)
                for (int i = booking.TidFra > earliestAllowedBooking ? booking.TidFra : earliestAllowedBooking; i < (booking.TidTil < latestAllowedBooking ? booking.TidTil : latestAllowedBooking); i++)
                    minutesAvailable[i - earliestAllowedBooking]++;
            // get how many groups can be in the room at a given point in time
            int maxGroups = new LokaleService().Read(_lokaleId).MaxGrupperAdGangen;
            // we then need to convert them to an interval of minutes
            int available = minutesAvailable[0];
            int startInterval = 0;
            for (int i = 0; i < minutesAvailable.Length-1; i++)
            {
                if (minutesAvailable[i] != available)
                {
                    int endInterval = i - 1;
                    availability.Add(new TidsInterval(startInterval + earliestAllowedBooking, endInterval + earliestAllowedBooking, available == 0 ? TidsInterval.AvailableType.Available : (available < maxGroups ? TidsInterval.AvailableType.PartiallyAvailable : TidsInterval.AvailableType.NotAvailable))); //minutesAvailable gives "false" when it is available
                    startInterval = i;
                    available = minutesAvailable[i];
                }
            }
            // add final interval
            {
                int endInterval = minutesAvailable.Length-1;
                availability.Add(new TidsInterval(startInterval + earliestAllowedBooking, endInterval + earliestAllowedBooking, available == 0 ? TidsInterval.AvailableType.Available : (available < maxGroups ? TidsInterval.AvailableType.PartiallyAvailable : TidsInterval.AvailableType.NotAvailable)));
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
            public AvailableType Available { get; }
            public TidsInterval(int start, int end, AvailableType available)
            {
                Start = start;
                End = end;
                Available = available;
            }
            public enum AvailableType
            {
                Available,
                PartiallyAvailable,
                NotAvailable,
            }
        }
    }
}

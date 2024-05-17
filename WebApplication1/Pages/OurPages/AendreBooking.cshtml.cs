using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages.OurPages
{
    public class AendreBookingModel : PageModel
    {
        private Booking SelectedBooking { get; set; }
        public int LokaleNummer
        {
            get
            {
                return SelectedBooking.LokaleId;
            }
        }
        public DateOnly Dato
        {
            get
            {
                return SelectedBooking.Dato;
            }
        }
        [BindProperty]
        public int StartTidHour { get; set; }
        public int StartTidMin { get; set; }
        public int EndTidHour { get; set; }
        public int EndTidMin { get; set; }
        public IActionResult OnGet(int bookingId)
        {
            System.Diagnostics.Debug.WriteLine("\n" + bookingId + "\n");
            SelectedBooking = new Services.BookingService().Read(bookingId);
            if (SelectedBooking == null)
                return RedirectToPage("BrugerSide");
            StartTidHour = SelectedBooking.TidFra / 60;
            StartTidMin = SelectedBooking.TidFra % 60;

            EndTidHour = (SelectedBooking.TidTil - SelectedBooking.TidFra) / 60;
            EndTidMin = (SelectedBooking.TidTil - SelectedBooking.TidFra) % 60;
            return Page();
        }
        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine(StartTidHour + " : " + StartTidMin + "->" + EndTidHour + " : " + EndTidMin);
            //Services.BookingService.CreateAcceptMessage();
            return Page();
        }

    }
}

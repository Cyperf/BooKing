// Jeppe
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.SQL;

namespace WebApplication1.Pages.OurPages
{
    public class AendreBookingModel : PageModel
    {
        private Booking SelectedBooking { get; set; }
        public int BookingId
        {
            get
            {
                return SelectedBooking.Id;
            }
        }

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
        public string Message { get; private set; }
        [BindProperty]
        public int StartTidHour { get; set; }
        [BindProperty]
        public int StartTidMin { get; set; }
        [BindProperty]
        public int EndTidHour { get; set; }
        [BindProperty]
        public int EndTidMin { get; set; }
        public IActionResult OnGet(int bookingId, string message = "")
        {
            SelectedBooking = new Services.BookingService().Read(bookingId);
            if (SelectedBooking == null || SelectedBooking.Gruppemedlem != LoginManager.LoggedInUser.Email)
                return RedirectToPage("BrugerSide");
            Message = message;
            StartTidHour = SelectedBooking.TidFra / 60;
            StartTidMin = SelectedBooking.TidFra % 60;

            EndTidHour = (SelectedBooking.TidTil - SelectedBooking.TidFra) / 60;
            EndTidMin = (SelectedBooking.TidTil - SelectedBooking.TidFra) % 60;
            return Page();
        }
        public IActionResult OnPost(int bookingId)
        {
            Services.BookingService service = new Services.BookingService();
            SelectedBooking = service.Read(bookingId);
            // delete the old booking
            service.Delete(bookingId);
            // attempt to place the new booking
            int startTid = StartTidHour * 60 + StartTidMin;
            int endTid = startTid + EndTidHour * 60 + EndTidMin;
            string message = "";
            if ((message = service.TryCreate(new Booking(-1, SelectedBooking.Dato, startTid, endTid, SelectedBooking.Gruppemedlem, SelectedBooking.LokaleId, SelectedBooking.SkoleId, SelectedBooking.BookingType)))
                == BookingService.CreateAcceptMessage())
            {
                // the new booking was created without problems
                return RedirectToPage("BrugerSide");

            }
            // could not create the new booking (place the old booking back in the database, and get its new id)
            service.TryCreate(SelectedBooking);
            AdoNet.ExecuteQuery($"SELECT Id FROM Booking WHERE TidFra={SelectedBooking.TidFra} AND TidTil={SelectedBooking.TidTil} AND BrugerEmail='{SelectedBooking.Gruppemedlem}' AND LokaleId={SelectedBooking.LokaleId} AND SkoleId={SelectedBooking.SkoleId} AND Type={SelectedBooking.BookingType.Id} ", reader => {
                if (!reader.Read())
                {
                    bookingId = -1;
                    return;
                }
                bookingId = reader.GetInt32(0); // we are only selecting the id
            });
            if (bookingId == -1)// something went wrong :(
                return RedirectToPage("BrugerSide");
            //int bookingId = SelectedBooking.Id;
            System.Diagnostics.Debug.WriteLine($"\nbookingId={bookingId}\nSelectedBooking:" + SelectedBooking + "\n" + StartTidHour + " : " + StartTidMin + "->" + EndTidHour + " : " + EndTidMin);
            return RedirectToPage(null, new { bookingId = bookingId, message = message });
        }

    }
}

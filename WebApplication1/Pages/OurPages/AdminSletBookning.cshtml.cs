using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using WebApplication1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Pages.OurPages
{
    public class AdminSletBookningModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Dato")]
        public DateOnly Date { get; set; }
        //private static DateOnly staticDate { get; set; }
        [BindProperty]
        [Display(Name = "Skole")]
        public int SchoolId { get; set; }
        [BindProperty]
        public string Message { get; set; } = string.Empty;
        public IActionResult OnGet(int year = 0, int month = 0, int day = 0, int schoolId = -1)
        {
            if (LoginManager.LoggedInUser.Rolle.DagesVarselIndenOverskrivelse == null)
                return RedirectToPage("../Account/AccessDenied");
            if (schoolId == -1)
                schoolId = LoginManager.LoggedInUser.SkoleId;
            if (year==0 && month==0 && day==0)
            {
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
                return RedirectToPage("AdminSletBookning", new { year = currentDate.Year, month = currentDate.Month, day = currentDate.Day, schoolId = schoolId });
            }
            Date = new DateOnly(year, month, day);
            SchoolId = schoolId;
            return Page();
        }
        public IActionResult OnPostFilter()
        {
            return RedirectToPage(null, new { year = Date.Year, month = Date.Month, day = Date.Day, schoolId = SchoolId });
        }
        public IActionResult OnPostDeleteBooking(int bookingId)
        {
            Booking? booking = new BookingService().Read(bookingId);
            if (booking == null)
            {
                Message = $"Der er sket en fejl, vær sød at prøve igen.";
                return RedirectToPage("AdminSletBookning", new { year = Date.Year, month = Date.Month, day = Date.Day, schoolId = SchoolId });
            }
            Message = $"Succesfult slettede booking på {booking.LokaleId}.";
            new BookingService().Delete(bookingId);
            return RedirectToPage("AdminSletBookning", new { year = Date.Year, month = Date.Month, day = Date.Day, schoolId = SchoolId });
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            // make sure the date is before the user can delete the booking
            if (Date < DateOnly.FromDateTime(DateTime.Now).AddDays(LoginManager.LoggedInUser.Rolle.DagesVarselIndenOverskrivelse.Value))
                return new Booking[0];
            return new BookingService().ReadAll($"Dato='{Date.Year + "-" + Date.Month + "-" + Date.Day}' AND SkoleId={SchoolId}")
                .OrderBy(booking => booking.LokaleId)
                .ThenBy(booking => booking.TidFra);
            //return null;
        }
    }
}

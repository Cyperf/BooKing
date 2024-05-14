using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class BrugerSideModel : PageModel
    {

        //{
        //    public static WebApplication1.Models.Bruger? LoggedInUser { get; private set; } = null;
        //}

        private readonly BookingService _bookingService;

        public BrugerSideModel()
        {
            _bookingService = new BookingService();
        }

        public IActionResult OnGet()
        {
            if (LogInModel.LoggedInBruger == null)
            {
                return RedirectToPage("LogIn");
            }
            return Page();
        }

        public IActionResult OnPostDeleteBooking(int bookingId)
        {

            _bookingService.Delete(bookingId);


            return RedirectToPage("BrugerSide");
        }

        public IActionResult OnPostSletBruger(string email)
        {
            var brugerService = new BrugerService();
            brugerService.DeleteByEmail(email);
            return RedirectToPage("LogIn");
        }

    }
}










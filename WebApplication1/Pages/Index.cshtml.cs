// Frederik og Jeppe
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            Thread cleanUpThread = new Thread(CleanUp);
            cleanUpThread.Start();
            return RedirectToPage("OurPages/LogIn");
        }

        private void CleanUp()
        {
            new BookingService().CleanOldBookings();
            new BrugerService().CleanOldAccounts();
        }
    }
}

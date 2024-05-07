using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class BookingModel : PageModel
    {
        private SkoleService _skoleService;

		public string skole;
        public DateOnly date;
        public int StartInterval { get; set; } = 0; // 0 minutes after midnight
        public int EndInterval { get; set; } = 1 * 60 * 60 * 24; // 24 hours after midnight (in minutes)
		public BookingModel(SkoleService skoleService)
        {
            _skoleService = skoleService;
        }
        public void OnGet(int startInterval, int endInterval)
        {
            skole = _skoleService.Read(LoginManager.LoggedInUser.SkoleId).Location;


		}
    }
}

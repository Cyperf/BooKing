using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class AdminRedigeringAfBrugerModel : PageModel
    {
        public Bruger bruger {  get; set; } = new Bruger();
        public BrugerService brugerService { get; set; } = new BrugerService();

        public void OnGet()
        {

            brugerService = new BrugerService();
        }

        public IActionResult OnPostSletBruger(string email)
        {
            string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var brugerService = new BrugerService();
            brugerService.DeleteByEmail(email);
            return Page();
        }

        public IActionResult OnPostRedigerBruger(string email)
        {
            string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var brugerService = new BrugerService();
            brugerService.DeleteByEmail(email);
            return RedirectToPage("RedigerBrugerAdmin");
        }

    }
}

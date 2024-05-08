using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Pages.OurPages //* Lavet af Roman
{
    public class LogInModel : PageModel
    {

        public static Bruger LoggedInBruger { get; set; }

        [BindProperty]

        public string Email { get; set; }

        [BindProperty]
        public string Kodeord { get; set; }

        [BindProperty]
        public string Message { get; set; }

        // [BindProperty]
        // public BrugerRolle Rolle { get; set; }

        public void OnGet()
        {
            Message = ""; //sletter tidligere fejlmeddelse, når siden er loadet.
        }
        public async Task<IActionResult> OnPost()
        {
            // System.Diagnostics.Debug.WriteLine("tried to log in withabc:\n" + Email + " : " + Kodeord);

            if (!WebApplication1.Services.LoginManager.Login(Email, Kodeord))
            {
                Message = "Fejl: Invalid login";
                return Page();
            }
            LoggedInBruger = WebApplication1.Services.LoginManager.LoggedInUser;



            //if (LoggedInBruger == null)
            //{
            //    Message = "Fejl: Invalid login";
            //    return Page();
            //}

            //Login med identity - note til mig selv

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                  BuildClaimsPrincipalFromUser(LoggedInBruger));

            return RedirectToPage("BrugerSide");

        }
        private ClaimsPrincipal BuildClaimsPrincipalFromUser(Bruger bruger)
        {
            List<Claim> Claims = new List<Claim>
            {
            new Claim(ClaimTypes.Email, bruger.Email),
            new Claim(ClaimTypes.Role, bruger.Rolle.RolleNavn),
            };

            ClaimsIdentity ClaimsIdentity = new ClaimsIdentity(Claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(ClaimsIdentity);
        }
    }
}
  


// Roman
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {

            LogInModel.LoggedInBruger = null;
            LoginManager.Logout();


            string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            await HttpContext.SignOutAsync(authScheme);


            return RedirectToPage("/OurPages/Login");
        }
    }
}

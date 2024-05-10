using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.OurPages
{
    public class BrugerSideModel : PageModel
    {

        //{
        //    public static WebApplication1.Models.Bruger? LoggedInUser { get; private set; } = null;
        //}

        public IActionResult OnGet()
        {
            if(LogInModel.LoggedInBruger == null)
            {
                return RedirectToPage("/OurPages/LogIn");
            } return Page();
        }
    }
}

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

        public IActionResult OnPostSletBruger(Bruger bruger)
        {
            brugerService.DeleteByEmail(bruger.Email);
            return Page();
        }
    }
}

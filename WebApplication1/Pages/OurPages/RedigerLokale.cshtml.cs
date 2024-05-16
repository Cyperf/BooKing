using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Pages.OurPages
{
    public class RedigerLokale : PageModel
    {
        private readonly LokaleService _lokaleservice;
        public Lokale Lokale { get; set; }

        public RedigerLokale(LokaleService lokaleservice)
        {
            _lokaleservice = lokaleservice;
        }

        public IActionResult OnGet(int Id)
        {
            Lokale = _lokaleservice.Read(Id, LoginManager.LoggedInUser.SkoleId);
            if (Lokale == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            
            return RedirectToPage("/Brugerside");
        }
    }
}




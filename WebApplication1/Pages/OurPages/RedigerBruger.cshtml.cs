// Frederik
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class RedigerBrugerModel : PageModel
    {
        public BrugerService _brugerService { get; set; } = new BrugerService();

        [BindProperty]
        public Bruger _bruger { get; set; }
        [Display(Name = "Kodeord")]
        [BindProperty]
        public string Kodeord { get; set; }
        [Display(Name = "GentagKodeord")]
        [BindProperty]
        public string GentagKodeord { get; set; }
        [BindProperty]
        public string Message { get; set; }


        public IActionResult OnGet()
        {
            if (LogInModel.LoggedInBruger == null)
            { return RedirectToPage("/OurPages/LogIn"); }
            return Page();
        }
        public IActionResult OnPost()
        {
            _bruger = LogInModel.LoggedInBruger;
            if(GentagKodeord == Kodeord) 
            { 
            _bruger.Kodeord = Kodeord;
            _brugerService.Update(_bruger);
            Message = "Kode skiftet";
            return Page();
            } else
            {
                Message = $"Kodeord matcher ikke";
                return Page();
            }
        }
    }
}

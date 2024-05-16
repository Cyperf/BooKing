using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class RedigerBrugerAdminModel : PageModel
    {
        public BrugerService _brugerService { get; set; } = new BrugerService();
        private string _email = "";

        [BindProperty]
        public Bruger _bruger { get; set; } = null;
        [Display(Name = "Kodeord")]
        [BindProperty]
        public string Kodeord { get; set; }
        [Display(Name = "GentagKodeord")]
        [BindProperty]
        public string GentagKodeord { get; set; }
        [BindProperty]
        public string Message { get; set; }


        public void OnGet(string email = "")
        {
            _email = email;
            _bruger = _brugerService.Read(email);
            Debug.WriteLine("\n1\n\n" + _bruger + " \nEmail: " + _email);
        }
        public IActionResult OnPost()
        {
            _bruger = _brugerService.Read(_bruger.Email);
            Debug.WriteLine("\n2\n\n" + _bruger + " \nEmail: " + _email);
            if (GentagKodeord == Kodeord)
            {
                _bruger.Kodeord = Kodeord;
                _brugerService.Update(_bruger);
                Message = $"Kode skiftet {_bruger.Email}";
                return Page();
            }
            else
            {
                Message = $"Kodeord matcher ikke{_bruger.Email}";
                return Page();
            }
        }
    }
}


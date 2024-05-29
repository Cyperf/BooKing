// Frederik
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
        public BrugerRolleService _brugerRolleService { get; set; } = new BrugerRolleService();

        public List<BrugerRolle> alleBrugerRoller { get; set; } = new List<BrugerRolle>();
        public SkoleService skoleService { get; set; } = new SkoleService();
        public List<Skole> AlleSkoler { get; set; } = new List<Skole>();

        [BindProperty]
        public Bruger _bruger { get; set; } = null;
        [BindProperty]
        public string Navn { get; set; }
        [Display(Name = "Email")]
        [BindProperty]
        public string Email { get; set; }
        [Display(Name = "Kodeord")]
        [BindProperty]
        public string Kodeord { get; set; }
        [Display(Name = "GentagKodeord")]
        [BindProperty]
        public string GentagKodeord { get; set; }
        [BindProperty]
        public int Rolle { get; set; }
        [BindProperty]
        public int SkoleId { get; set; }
        [BindProperty]
        public DateOnly SletningsDato { get; set; }
        [BindProperty]
        public string Message { get; set; }
        


        public void OnGet(string email = "")
        {
            _bruger = _brugerService.Read(email);
            Debug.WriteLine("\n1\n\n" + _bruger + " \nEmail: " + email);
            foreach (Skole skole in skoleService.ReadAll())
            {
                AlleSkoler.Add(skole);
            }

            foreach (BrugerRolle Rolle in _brugerRolleService.ReadAll())
            {
                alleBrugerRoller.Add(Rolle);
            }
            Navn = _bruger.Navn;
            Rolle = _bruger.Rolle.Id;
            SkoleId = _bruger.SkoleId;
            SletningsDato = _bruger.SletningsDato;

        }
        public IActionResult OnPost()
        {
            if (new BrugerRolleService().Read(Rolle) == null)
                return RedirectToPage("/OurPages/RedigerBrugerAdmin", new { message = "Du skal vælge en rolle" });
            // make sure the school exists
            if (new SkoleService().Read(SkoleId) == null)
                return RedirectToPage("/OurPages/RedigerBrugerAdmin", new { message = "Du skal vælge en skole" });
            Debug.WriteLine("\n2\n\n" + _bruger + " \nEmail: "  );
            if (GentagKodeord == Kodeord)
            {
                _bruger.Kodeord = Kodeord;
                _bruger.Navn = Navn;
                _bruger.Email = Email;
                _bruger.Kodeord = Kodeord;
                _bruger.Rolle = new BrugerRolleService().Read(Rolle);
                _bruger.SkoleId = SkoleId;
                _bruger.SletningsDato = SletningsDato;
                _brugerService.AdminUpdate(_bruger);
                Message = $"Bruger skiftet";
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


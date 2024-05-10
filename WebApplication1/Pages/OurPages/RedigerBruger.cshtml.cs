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
        public BrugerRolleService _brugerRolleService { get; set; } = new BrugerRolleService();

        public List<BrugerRolle> alleBrugerRoller { get; set; } = new List<BrugerRolle>();
        public SkoleService skoleService { get; set; } = new SkoleService();
        public List<Skole> AlleSkoler { get; set; } = new List<Skole>();

        [BindProperty]
        public Bruger _bruger { get; set; }
        [Display(Name = "Navn")]
        [BindProperty]
        public string Navn { get; set; } = LogInModel.LoggedInBruger.Navn;
        [Display(Name = "Email")]
        [BindProperty]
        public string Email { get; set; } = LogInModel.LoggedInBruger.Email;
        [Display(Name = "Kodeord")]
        [BindProperty]
        public string Kodeord { get; set; } = LogInModel.LoggedInBruger.Kodeord;
        [Display(Name = "Rolle")]
        [BindProperty]
        public int Rolle { get; set; } = LogInModel.LoggedInBruger.brugerId;
        [BindProperty]
        public int SkoleId { get; set; } = LogInModel.LoggedInBruger.SkoleId;
        [BindProperty]
        public DateOnly SletningsDato { get; set; } = LogInModel.LoggedInBruger.SletningsDato;

        public IActionResult OnGet()
        {
            if (LogInModel.LoggedInBruger == null)
            { return RedirectToPage("/OurPages/LogIn"); }

            foreach (Skole skole in skoleService.ReadAll())
            {
                AlleSkoler.Add(skole);
            }

            foreach (BrugerRolle Rolle in _brugerRolleService.ReadAll())
            {
                alleBrugerRoller.Add(Rolle);
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            _bruger.Navn = Navn;
            _bruger.Email = Email;
            _bruger.Kodeord = Kodeord;
            _bruger.brugerId = Rolle;
            _bruger.SkoleId = SkoleId;
            _bruger.SletningsDato = SletningsDato;
            _brugerService.Update(_bruger);
            return RedirectToPage("/OurPages/OpretBrugerKonto");
        }
    }
}

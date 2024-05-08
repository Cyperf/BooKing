using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class OpretBrugerKontoModel : PageModel
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
        public string Navn { get; set; }
        [Display(Name = "Email")]
        [BindProperty]
        public string Email { get; set; }
        [Display(Name = "Kodeord")]
        [BindProperty]
        public string Kodeord { get; set; }
        [Display(Name = "Rolle")]
        [BindProperty]
        public BrugerRolle Rolle { get; set; }
        [BindProperty]
        public int SkoleId { get; set; }
        [BindProperty]
        public DateOnly SletningsDato { get; set; }

        public IActionResult OnGet()
        {
            //KUN ADMINS PLEASE AND TY

            foreach (Skole skole in skoleService.ReadAll())
            {
                AlleSkoler.Add(skole);
            }

            foreach (BrugerRolle rolle in _brugerRolleService.ReadAll())
            {
                alleBrugerRoller.Add(rolle);
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            Kodeord = "yeehaw";//Random password generator.. skal nok udbygges
            _bruger.Navn = Navn;
            _bruger.Email = Email;
            _bruger.Kodeord = Kodeord;
            _bruger.Rolle = Rolle;
            _bruger.SkoleId = SkoleId;
            _bruger.SletningsDato = SletningsDato;
            _brugerService.Create(_bruger);
            return RedirectToPage("/OurPages/OpretBrugerKonto");
        }


    }
}

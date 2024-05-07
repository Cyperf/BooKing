using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class OpretBrugerKontoModel : PageModel
    {
        public BrugerService _brugerService { get; set; } = new BrugerService();
        public BrugerRolleService _brugerRolleService { get; set; } = new BrugerRolleService();

        public List<BrugerRolle> brugerRoller { get; set; } = new List<BrugerRolle>();

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
           
            foreach(BrugerRolle rolle in _brugerRolleService.ReadAll())
            {
                brugerRoller.Add(rolle);
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            _bruger.Navn = Navn;
            _bruger.Email = Email;
            _bruger.Kodeord = Kodeord;
            _bruger.Rolle = Rolle;
            _bruger.SkoleId = SkoleId;
            _bruger.SletningsDato = SletningsDato;
            _brugerService.Create(_bruger);
            return RedirectToPage("/OurPages/OpretBrugerKonto");
        }

   //     Navn = navn;
   //         Email = email;
   //         Kodeord = kodeord;
   //         Rolle = rolle;
   //         SkoleId = skoleId;
			//SletningsDato = sletningsDato;

    }
}

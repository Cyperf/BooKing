// Frederik
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    [Authorize(Roles ="admin")]
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
        public string Kodeord { get; set; } = GetRandomPassword();
        [Display(Name = "Rolle")]
        [BindProperty]
        public int Rolle { get; set; }
        [BindProperty]
        public int SkoleId { get; set; }
        [BindProperty]
        public DateOnly SletningsDato { get; set; }
        [BindProperty]
        public string Message { get; set; } = "";

        public IActionResult OnGet(string message = "")
        {
            Message = message;
            SletningsDato = DateOnly.FromDateTime(DateTime.Now);
            if (LogInModel.LoggedInBruger == null || LogInModel.LoggedInBruger.Rolle.RolleNavn != "admin")
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
           if (string.IsNullOrEmpty(Navn))
                return RedirectToPage("/OurPages/OpretBrugerKonto", new { message = "Du skal skrive et navn" });
           // make sure the email most likely makes sense
            if (Email == null || !(Email.Length > 5 && Email.Contains('@') && Email.Contains('.')))
                return RedirectToPage("/OurPages/OpretBrugerKonto", new { message = "Du skal indtaste en valid email" });
            if (Kodeord == null || Kodeord.Length < 5)
                return RedirectToPage("/OurPages/OpretBrugerKonto", new { message = "Du skal skrive en kode med mindst 5 karaktere" });
            // make sure the role exists
            if (new BrugerRolleService().Read(Rolle) == null)
                return RedirectToPage("/OurPages/OpretBrugerKonto", new { message = "Du skal vælge en rolle" });
            // make sure the school exists
            if (new SkoleService().Read(SkoleId) == null)
                return RedirectToPage("/OurPages/OpretBrugerKonto", new { message = "Du skal vælge en skole" });
            // Make sure the deletion date is not today, or prior to today
            if (SletningsDato <= DateOnly.FromDateTime(DateTime.Now))
                return RedirectToPage("/OurPages/OpretBrugerKonto", new { message = "Du skal vælge sletningsdatoen mindst 1 dag i fremtiden" });
            _bruger.Navn = Navn;
            _bruger.Email = Email;
            _bruger.Kodeord = Kodeord;
            _bruger.Rolle = new BrugerRolleService().Read(Rolle);
            _bruger.SkoleId = SkoleId;
            _bruger.SletningsDato = SletningsDato;
            _brugerService.Create(_bruger);
            return RedirectToPage("/OurPages/OpretBrugerKonto", new { message = "Brugeren blev oprettet" });
        }

        private static string GetRandomPassword()
        {
            // setup a string, with all allowed chars
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // create the password
            Random random = new Random();
            string password = "";
            int passwordLength = 8;
            for (int i = 0; i < passwordLength; i++)
                password += allowedChars[random.Next(allowedChars.Length)];

            return password;
        }


    }
}

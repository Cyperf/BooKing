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
        public string Kodeord { get; set; } = GetRandomPassword();
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

            foreach (BrugerRolle Rolle in _brugerRolleService.ReadAll())
            {
                alleBrugerRoller.Add(Rolle);
            }
            return Page();
        }
        public IActionResult OnPost()
        {
           //for(int i= 0;  i > 8; i++)
           // {
           //     int o = Random(1, 61);

           //     if (o < 10){
           //         Kodeord = Kodeord + $"{o}";
           //     } else if (o < 35)
           //     {
           //         Kodeord = Kodeord + " ";
           //     } else if {
           //         Kodeord = Kodeord "":
           //     }
           // }

            _bruger.Navn = Navn;
            _bruger.Email = Email;
            _bruger.Kodeord = Kodeord;
            _bruger.Rolle = Rolle;
            _bruger.SkoleId = SkoleId;
            _bruger.SletningsDato = SletningsDato;
            _brugerService.Create(_bruger);
            Debug.WriteLine(Rolle.ToString());
            return RedirectToPage("/OurPages/OpretBrugerKonto");
        }

        private static string GetRandomPassword()
        {
            // setup an array, with all allowed chars
            int alphabetSize = 'z' - 'a' + 1;
            char[] allowedChars = new char[alphabetSize * 2 + '9' - '0' + 1];
            for (int i = 0; i < alphabetSize; i++)
                allowedChars[i] += (char)('a' + i);
            for (int i = 0; i < alphabetSize; i++)
                allowedChars[i + alphabetSize] += (char)('A' + i);
            for (int i = 0; i < 10; i++)
                allowedChars[i + alphabetSize * 2] += (char)('0' + i);

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

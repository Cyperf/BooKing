using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.SQL;

namespace WebApplication1.Pages.OurPages
{
    [Authorize(Roles = "admin")]
    public class OpretLokalerModel : PageModel
    {

        public LokaleService lokaleService { get; set; } = new LokaleService();
        public List<Skole> AlleSkoler { get; set; } = new List<Skole>();
        public SkoleService skoleService { get; set; } = new SkoleService();
        
        [BindProperty]
        public Lokale _lokale { get; set; }
        [Display(Name = "Lokale nummer")]
        [BindProperty]
        public int Id { get; set; }
        [Display(Name ="SkoleId")]
        [BindProperty]
        public int SkoleId { get; set; }
        [Display(Name="Max grupper ad gangen")]
        [BindProperty]
        public int MaxGrupperAdGangen { get; set; }
        [Display(Name="Har Smartboard")]
        [BindProperty]
        public bool HarSmartboard { get; set; }
        [BindProperty]
        public string Message { get; set; }


        public IActionResult OnGet(string message = "")
        {
            Message = message;
            if (LogInModel.LoggedInBruger == null || LogInModel.LoggedInBruger.Rolle.RolleNavn != "admin")
            { return RedirectToPage("/OurPages/LogIn"); }

            foreach (Skole skole in skoleService.ReadAll())
            {
                AlleSkoler.Add(skole);
            }
            return Page();

        }

        public IActionResult OnPostAdd() 
        {
            if (lokaleService.Read(Id, SkoleId) != null)
                return RedirectToPage("/OurPages/OpretLokaler", new { message = "Et lokale med samme lokale numme på samme skole eksistere allerede" });
            _lokale.Id = Id;
            _lokale.SkoleId = SkoleId;
            _lokale.MaxGrupperAdGangen = MaxGrupperAdGangen;
            _lokale.HarSmartBoard = HarSmartboard;
            lokaleService.Create(_lokale);
            if (lokaleService.Read(Id, SkoleId) != null)
                return RedirectToPage("/OurPages/OpretLokaler", new { message = "Lokalet blev oprettet korrekt" });
            return RedirectToPage("/OurPages/OpretLokaler", new { message = "Noget gik galt, prøv igen" });
        }

    }
}

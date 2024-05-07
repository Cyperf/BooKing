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
    public class OpretLokalerModel : PageModel
    {

        public LokaleService _lokaleService { get; set; } = new LokaleService();
        public List<Skole> AlleSkoler { get; set; } = new List<Skole>();
        public SkoleService skoleService { get; set; } = new SkoleService();
        
        [BindProperty]
        public Lokale _lokale { get; set; }
        [Display(Name = "Id")]
        [BindProperty]
        public int Id { get; set; }
        [Display(Name ="SkoleId")]
        [BindProperty]
        public int SkoleId { get; set; }
        [Display(Name="MaxGrupperAdGangen")]
        [BindProperty]
        public int MaxGrupperAdGangen { get; set; }
        [Display(Name="Har Smartboard")]
        [BindProperty]
        public bool HarSmartboard { get; set; }


        public IActionResult OnGet()
        {
            // make only admins allowed BG
            foreach (Skole skole in skoleService.ReadAll())
            {
                AlleSkoler.Add(skole);
            }
            return Page();

        }

        public IActionResult OnPostAdd() 
        {
            _lokale.Id = Id;
            _lokale.SkoleId = SkoleId;
            _lokale.MaxGrupperAdGangen = MaxGrupperAdGangen;
            _lokale.HarSmartBoard = HarSmartboard;
            _lokaleService.Create(_lokale);
            return RedirectToPage("/OurPages/OpretLokaler");
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.SQL;

namespace WebApplication1.Pages.OurPages
{
    public class OpretLokalerModel : PageModel
    {

        public LokaleService _lokaleService { get; set; }
        public Lokale _lokale { get; set; }
        public List<Skole> AlleSkoler { get; set; }
        public SkoleService skoleService { get; set; }


        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name ="SkoleId")]
        public int SkoleId { get; set; }
        [Display(Name="MaxGrupperAdGangen")]
        public int MaxGrupperAdGangen { get; set; }
        [Display(Name="Har Smartboard")]
        public bool HarSmartboard { get; set; }
        public IActionResult OnGet()
        {
            // make only admins allowed BG
            foreach(Skole skole in skoleService.ReadAll("Skole"))
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
            return Page();
        }

    }
}

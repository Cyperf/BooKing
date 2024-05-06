using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.OurPages
{
    public class OpretLokalerModel : PageModel
    {

        //public LokaleService _lokaleService { get; set; }
        public Lokale _lokale { get; set; }


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
            return Page();

        }

        public IActionResult OnPostAdd() 
        {
            _lokale.Id = Id;
            _lokale.SkoleId = SkoleId;
            _lokale.MaxGrupperAdGangen = MaxGrupperAdGangen;
            _lokale.HarSmartBoard = HarSmartboard;
            //_lokaleService.Create(_lokale);
            return Page();
        }

    }
}

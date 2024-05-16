using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages.OurPages
{
    public class RedigerLokale : PageModel
    {
        public Lokale Lokale { get; set; }
        [Display(Name = "Lokale nummer")]
        [BindProperty]
        public int Id { get; set; }
        [Display(Name = "SkoleId")]
        [BindProperty]
        public int SkoleId { get; set; }
        [Display(Name = "Max grupper ad gangen")]
        [BindProperty]
        public int MaxGrupperAdGangen { get; set; }
        [Display(Name = "Har Smartboard")]
        [BindProperty]
        public bool HarSmartboard { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public RedigerLokale()
        {
            SkoleId = LoginManager.LoggedInUser.SkoleId;
        }
        public IActionResult OnGet(int SkoleId = -1)
        {
            if (SkoleId == -1)
                SkoleId = LoginManager.LoggedInUser.SkoleId;
            this.SkoleId = SkoleId;
            return Page();
        }

        //public IActionResult OnGet(int Id)
        //{
        //    Lokale = _lokaleservice.Read(Id, LoginManager.LoggedInUser.SkoleId);
        //    if (Lokale == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}

        public IActionResult OnPostChange()
        {
            // make sure we picked a valid room
            LokaleService lokaleService = new LokaleService();
            if (lokaleService.Read(Id, SkoleId) == null)
            {
                Message = "Lokalet kunne ikke findes";
                return Page();
            }
            lokaleService.Update(new Lokale(Id, SkoleId, MaxGrupperAdGangen, HarSmartboard));
            Message = "Lokalet blev redigeret";
            return Page();
        }
        public IActionResult OnPostDelete()
        {
            // make sure we picked a valid room
            LokaleService lokaleService = new LokaleService();
            if (lokaleService.Read(Id, SkoleId) == null)
            {
                Message = "Lokalet kunne ikke findes";
                return Page();
            }
            lokaleService.Delete(Id, SkoleId);

            Message = "Lokalet blev slettet";
            return Page();
        }
    }
}




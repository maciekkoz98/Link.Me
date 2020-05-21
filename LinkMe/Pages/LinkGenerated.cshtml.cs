using LinkMe.Core;
using LinkMe.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkMe.Pages
{
    public class LinkGeneratedModel : PageModel
    {
        private readonly ILinkData linkData;

        public Link Link { get; set; }

        public LinkGeneratedModel(ILinkData linkData)
        {
            this.linkData = linkData;
        }

        public IActionResult OnGet([FromQuery] int id = -1)
        {
            if (id != -1)
            {
                Link = linkData.GetLinkByID(id);
            }
            else
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }


    }
}
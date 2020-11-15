using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkMe.Pages
{
    public class LinkGeneratedModel : PageModel
    {
        private readonly ILinkData linkData;

        public LinkGeneratedModel(ILinkData linkData)
        {
            this.linkData = linkData;
        }

        public Link Link { get; set; }

        public IActionResult OnGet([FromQuery] int id = -1)
        {
            if (id != -1)
            {
                this.Link = this.linkData.GetLinkByID(id);
            }
            else
            {
                return this.RedirectToPage("./Index");
            }

            if (this.Link == null)
            {
                return this.NotFound();
            }
            else if (!this.Link.ShownSummary)
            {
                this.Link.ShownSummary = true;
                return this.Page();
            }
            else
            {
                return this.StatusCode(403);
            }
        }
    }
}
using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LinkMe.Pages
{
    public class LinkGeneratedModel : PageModel
    {
        private readonly ILinkRepository linkRepository;

        public LinkGeneratedModel(ILinkRepository linkRepository)
        {
            this.linkRepository = linkRepository;
        }

        public Link Link { get; set; }

        public int DaysLeft { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] int id = -1)
        {
            if (id != -1)
            {
                this.Link = await this.linkRepository.GetByIdAsync(id);
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
                this.DaysLeft = (this.Link.ValidTo - DateTime.UtcNow).Days + 1;
                return this.Page();
            }
            else
            {
                return this.StatusCode(403);
            }
        }
    }
}
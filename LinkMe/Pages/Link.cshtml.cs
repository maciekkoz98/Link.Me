using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LinkMe.Pages
{
    public class LinkStatsModel : PageModel
    {
        private readonly ILinkRepository linkRepository;

        public LinkStatsModel(ILinkRepository linkRepository)
        {
            this.linkRepository = linkRepository;
        }

        public Link Link { get; set; }

        public int DaysLeft { get; set; }

        public async Task<IActionResult> OnGetAsync(int linkId)
        {
            this.Link = await this.linkRepository.GetFullLink(linkId);
            if (this.Link == null)
            {
                return this.RedirectToPage("./Error");
            }

            this.DaysLeft = (this.Link.ValidTo - DateTime.UtcNow).Days + 1;

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(int linkId)
        {
            this.Link = await this.linkRepository.GetByIdAsync(linkId);
            await this.linkRepository.DeleteAsync(this.Link);
            return this.Redirect("/");
        }
    }
}

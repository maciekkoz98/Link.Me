using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LinkMe.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ILinkRepository linkRepository;

        public IndexModel(ILogger<IndexModel> logger, ILinkRepository linkRepository)
        {
            this.logger = logger;
            this.linkRepository = linkRepository;
        }

        [BindProperty]
        public Link Link { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public async Task OnGetAsync()
        {
            var userIDClaim = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIDClaim != null)
            {
                this.Links = await this.linkRepository.GetLinksByUserId(userIDClaim.Value);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }
            else
            {
                var userIDClaim = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIDClaim == null)
                {
                    this.Link.GenerateShortLink(UserType.Unregistered);
                }
                else
                {
                    var userTypeClaim = this.User.Claims.FirstOrDefault(x => x.Type == "UserType");
                    this.Link.GenerateShortLink(Enum.Parse<UserType>(userTypeClaim.Value));
                    this.Link.OwnerId = userIDClaim.Value;
                }

                this.Link.ShownSummary = false;
                await this.linkRepository.AddAsync(this.Link);
                return this.RedirectToPage("./LinkGenerated", new { id = this.Link.Id });
            }
        }
    }
}

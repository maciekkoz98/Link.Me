using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LinkMe.Core;
using LinkMe.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LinkMe.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ILinkData linkData;

        public IndexModel(ILogger<IndexModel> logger, ILinkData linkData)
        {
            this.logger = logger;
            this.linkData = linkData;
        }

        [BindProperty]
        public Link Link { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public void OnGet()
        {
            var userIDClaim = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIDClaim != null)
            {
                this.Links = this.linkData.GetLinksByOwnerID(userIDClaim.Value);
            }
        }

        public IActionResult OnPost()
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
                    this.Link.OwnerID = userIDClaim.Value;
                }

                this.Link.ShownSummary = false;
                this.linkData.Add(this.Link);
                this.linkData.Commit();
                return this.RedirectToPage("./LinkGenerated", new { id = this.Link.ID });
            }
        }
    }
}

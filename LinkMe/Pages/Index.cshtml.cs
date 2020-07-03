using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly ILogger<IndexModel> _logger;
        private readonly ILinkData linkData;

        public IndexModel(ILogger<IndexModel> logger, ILinkData linkData)
        {
            _logger = logger;
            this.linkData = linkData;
        }

        [BindProperty]
        public Link Link { get; set; }
        public IEnumerable<Link> Links { get; set; }

        public void OnGet()
        {
            var userIDClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIDClaim != null)
            {
                Links = linkData.GetLinksByOwnerID(userIDClaim.Value);
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var userIDClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIDClaim == null)
                {
                    Link.GenerateShortLink(UserType.Unregistered);
                }
                else
                {
                    var userTypeClaim = User.Claims.FirstOrDefault(x => x.Type == "UserType");
                    Link.GenerateShortLink(Enum.Parse<UserType>(userTypeClaim.Value));
                    Link.OwnerID = userIDClaim.Value;
                }
                Link.ShownSummary = false;
                linkData.Add(Link);
                linkData.Commit();
                return RedirectToPage("./LinkGenerated", new { id = Link.ID });
            }
        }
    }
}

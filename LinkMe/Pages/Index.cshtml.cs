using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LinkMe.Core;
using LinkMe.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LinkMe.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILinkData linkData;

        [BindProperty]
        public Link Link { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ILinkData linkData)
        {
            _logger = logger;
            this.linkData = linkData;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                Link.GenerateShortLink(UserType.Unregistered);
                linkData.Add(Link);
                linkData.Commit();
                return RedirectToPage("./LinkGenerated", new { id = Link.ID });
            }
        }
    }
}

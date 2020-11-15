﻿using System.Threading.Tasks;
using LinkMe.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LinkMe.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<PersonalDataModel> logger;

        public PersonalDataModel(
            UserManager<User> userManager,
            ILogger<PersonalDataModel> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.Page();
        }
    }
}
using LinkMe.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LinkMe.Areas.Identity.Data
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> op)
            : base(userManager, op)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserType", user.UserType.ToString()));
            return identity;
        }
    }
}

using LinkMe.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace LinkMe.Core.Entities
{
    public class User : IdentityUser
    {
        [PersonalData]
        public UserType UserType { get; set; }

        [PersonalData]
        public string Firstname { get; set; }

        [PersonalData]
        public string Lastname { get; set; }
    }
}

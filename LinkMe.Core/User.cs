using Microsoft.AspNetCore.Identity;

namespace LinkMe.Core
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

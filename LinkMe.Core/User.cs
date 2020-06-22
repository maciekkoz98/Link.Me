using Microsoft.AspNetCore.Identity;
using System;

namespace LinkMe.Core   
{
    public class User : IdentityUser
    {
        [PersonalData]
        public UserType UserType { get; set; }
    }
}

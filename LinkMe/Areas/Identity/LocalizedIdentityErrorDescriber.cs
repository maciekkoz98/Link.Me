using Microsoft.AspNetCore.Identity;

namespace LinkMe.Areas.Identity
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(this.PasswordRequiresLower),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresLower,
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(this.PasswordRequiresUpper),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresUpper,
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(this.PasswordRequiresDigit),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresDigit,
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(this.PasswordRequiresNonAlphanumeric),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresNonAphabetic,
            };
        }

        public override IdentityError DuplicateUserName(string email)
        {
            return new IdentityError
            {
                Code = nameof(this.DuplicateEmail),
                Description = LocalizedIdentityErrorMessages.DuplicateUserName,
            };
        }
    }
}

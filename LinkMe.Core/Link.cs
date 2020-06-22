using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace LinkMe.Core
{
    public class Link : IValidatableObject
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nie podano linku")]
        [Url(ErrorMessage = "Podany link jest nieprawidłowy")]
        public string OriginalLink { get; set; }

        public string ShortLink { get; set; }
        public int OwnerID { get; set; }
        public string ValidTo { get; set; } //lub DateTime
        public bool ShownSummary { get; set; }

        public void GenerateShortLink(UserType userType)
        {
            StringBuilder linkToShort = new StringBuilder(OriginalLink);
            while (linkToShort.Length < 512)
            {
                linkToShort.Append(linkToShort);
            }
            var now = DateTime.UtcNow;
            linkToShort.Append(now.ToString());
            ShortLink = "https://link.me/" + Adler32(linkToShort.ToString()).ToString("X").ToLower();
            switch (userType)
            {
                case UserType.Unregistered:
                    ValidTo = now.AddDays(3).ToShortDateString();
                    break;
                case UserType.Registered:
                    ValidTo = now.AddDays(7).ToShortDateString();
                    break;
                case UserType.Premium:
                    ValidTo = "forever";
                    break;
            }
        }

        private int Adler32(string word)
        {
            const int mod = 65521;
            int a = 1, b = 0;
            foreach (char c in word)
            {
                a = (a + c) % mod;
                b = (b + a) % mod;
            }
            return (b << 16) | a;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ValidationResult validationResult;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(OriginalLink);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode >= HttpStatusCode.OK && response.StatusCode <= HttpStatusCode.PartialContent)
                {
                    validationResult = ValidationResult.Success;
                }
                else if (response.StatusCode >= HttpStatusCode.MultipleChoices && response.StatusCode <= HttpStatusCode.TemporaryRedirect)
                {
                    // TODO: Change OriginalLink if redirection occurs
                    validationResult = ValidationResult.Success;
                }
                else
                {
                    validationResult = new ValidationResult("Twój link prowadzi donikąd :(", new[] { nameof(OriginalLink) });
                }
            }
            catch (WebException)
            {
                validationResult = new ValidationResult("Twój link prowadzi donikąd :(", new[] { nameof(OriginalLink) });
            }

            yield return validationResult;
        }
    }
}

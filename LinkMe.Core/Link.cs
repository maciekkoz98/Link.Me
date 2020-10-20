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

        public string OwnerID { get; set; }

        public DateTime ValidTo { get; set; }

        public bool ShownSummary { get; set; }

        public void GenerateShortLink(UserType userType)
        {
            StringBuilder linkToShort = new StringBuilder(this.OriginalLink);
            while (linkToShort.Length < 512)
            {
                linkToShort.Append(linkToShort);
            }

            var now = DateTime.UtcNow;
            linkToShort.Append(now.ToString());
            this.ShortLink = this.Adler32(linkToShort.ToString()).ToString("X").ToLower();
            switch (userType)
            {
                case UserType.Unregistered:
                    this.ValidTo = now.AddDays(3);
                    break;
                case UserType.Registered:
                    this.ValidTo = now.AddDays(7);
                    break;
                case UserType.Premium:
                    this.ValidTo = now.AddYears(100);
                    break;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ValidationResult validationResult;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.OriginalLink);
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
                    validationResult = new ValidationResult("Twój link prowadzi donikąd :(", new[] { nameof(this.OriginalLink) });
                }
            }
            catch (WebException)
            {
                validationResult = new ValidationResult("Twój link prowadzi donikąd :(", new[] { nameof(this.OriginalLink) });
            }

            yield return validationResult;
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
    }
}

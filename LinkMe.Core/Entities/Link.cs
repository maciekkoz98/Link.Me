using LinkMe.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace LinkMe.Core.Entities
{
    public class Link : BaseEntity, IValidatableObject
    {
        [Required(ErrorMessage = "Nie podano linku")]
        [StringLength(200)]
        [Url(ErrorMessage = "Podany link jest nieprawidłowy")]
        public string OriginalLink { get; set; }

        [StringLength(15)]
        public string ShortLink { get; set; }

        [StringLength(450)]
        public string OwnerId { get; set; }

        public bool IsPremiumLink { get; set; }

        public DateTime ValidTo { get; set; }

        public bool ShownSummary { get; set; }

        public List<LinkClick> LinkClicks { get; set; }

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
                    this.IsPremiumLink = false;
                    this.ValidTo = now.AddDays(3);
                    break;
                case UserType.Registered:
                    this.IsPremiumLink = false;
                    this.ValidTo = now.AddDays(7);
                    break;
                case UserType.Premium:
                    this.IsPremiumLink = true;
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
                    this.OriginalLink = response.ResponseUri.AbsoluteUri;
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

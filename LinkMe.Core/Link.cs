using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

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

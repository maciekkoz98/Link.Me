using LinkMe.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace LinkMe.Areas.Identity
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            this.Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string callbackUrl)
        {
            return this.Execute(this.Options.SendGridKey, subject, email, callbackUrl);
        }

        public Task Execute(string apiKey, string subject, string email, string callbackUrl)
        {
            var client = new SendGridClient(apiKey);
            var templateData = new EmailTemplateData
            {
                CallbackUrl = callbackUrl,
            };
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("maciekkoz98@gmail.com", this.Options.SendGridUser),
                Subject = subject,
                TemplateId = "d-d9ef54f346ab40b2ab8ffbf0cec1ffb8",
            };
            msg.SetTemplateData(templateData);
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }

        private class EmailTemplateData
        {
            [JsonProperty("registrationUrl")]
            public string CallbackUrl { get; set; }
        }
    }
}

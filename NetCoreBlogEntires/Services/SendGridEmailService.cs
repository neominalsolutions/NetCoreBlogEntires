using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Services
{
    public class SendGridEmailService : IEmailService
    {
        public async Task SendEmailAsync(string from, string to, string subject, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRIDAPIKEY");
            var client = new SendGridClient(apiKey);
            var _from = new EmailAddress(from, "NBUY Öğlen");
            var _to = new EmailAddress(to, "mail user");
            var msg = MailHelper.CreateSingleEmail(_from, _to, subject,message,message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}

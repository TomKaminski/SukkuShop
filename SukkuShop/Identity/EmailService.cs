using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SukkuShop.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            const string credentialUserName = "sukkusklep@gmail.com";
            const string sentFrom = "sukkusklep@gmail.com";
            const string pwd = "sukku123";

            // Configure the client:
            var client =
                new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

            // Creatte the credentials:
            var credentials =
                new NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new MailMessage(sentFrom, message.Destination)
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true

                };

            // Send:
            return client.SendMailAsync(mail);
        }
    }
}
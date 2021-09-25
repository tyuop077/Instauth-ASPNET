using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
namespace Instauth.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Instauth Credentials", "noreply@tyuop.tk"));
            emailMessage.To.Add(new MailboxAddress("Instauth Owner", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("noreply@tyuop.tk", System.Environment.GetEnvironmentVariable("PASSWORD"));
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
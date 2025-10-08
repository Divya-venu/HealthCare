using HealthCareApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace HealthCareApp.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendInvitationEmail(string recipientEmail, string recipientName, string invitationLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HealthCare App", _config["Email:Username"]));
            message.To.Add(new MailboxAddress(recipientName, recipientEmail));
            message.Subject = "You're Invited to Join HealthCare App!";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<h1>Hello {recipientName},</h1><p>You have been invited to join the HealthCare App platform.</p><p>Please click the following link to register your account: <a href=\"{invitationLink}\">{invitationLink}</a></p><p>Thank you.</p>";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config["Email:SmtpServer"], int.Parse(_config["Email:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
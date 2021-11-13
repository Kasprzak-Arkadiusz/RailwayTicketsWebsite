using Application.Common.Interfaces;
using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit.Text;
using Application.Common.Models;

namespace Infrastructure.Services.EmailSender
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task SendEmailAsync(EmailAddress emailAddress, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(emailAddress.FullName, emailAddress.Address));
            message.From.Add(new MailboxAddress(_emailConfiguration.SenderName, _emailConfiguration.SenderEmail));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlMessage
            };

            using var emailClient = new SmtpClient();

            await emailClient.ConnectAsync(_emailConfiguration.HostSmtp, _emailConfiguration.Port, _emailConfiguration.EnableSsl);

            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

            await emailClient.AuthenticateAsync(_emailConfiguration.SenderEmail, _emailConfiguration.SenderEmailPassword);
            await emailClient.SendAsync(message);
            await emailClient.DisconnectAsync(true);
        }

    }
}
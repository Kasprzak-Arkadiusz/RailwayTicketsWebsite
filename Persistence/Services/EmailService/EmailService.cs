using Application.Common.Interfaces;
using Application.Common.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Infrastructure.Services.EmailService
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

        public async Task SendConfirmationEmailAsync(string callbackUrl, EmailAddress emailAddress)
        {
            const string subject = "Confirm your email";
            var htmlMessage = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
            await SendEmailAsync(emailAddress, subject, htmlMessage);
        }
    }
}
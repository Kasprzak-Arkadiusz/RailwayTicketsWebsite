using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailAddress emailAddress, string subject, string htmlMessage);

        Task SendConfirmationEmailAsync(string callbackUrl, EmailAddress emailAddress);
    }
}
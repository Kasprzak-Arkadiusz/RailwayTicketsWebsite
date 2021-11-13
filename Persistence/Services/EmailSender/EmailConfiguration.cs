using Application.Common.Interfaces;

namespace Infrastructure.Services.EmailSender
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string HostSmtp { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public string SenderName { get; set; }
    }
}
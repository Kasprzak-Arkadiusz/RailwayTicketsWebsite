namespace Application.Common.Models
{
    public class EmailAddress
    {
        public string FullName { get; }
        public string Address { get; }

        public EmailAddress(string firstName, string lastName, string address)
        {
            FullName = $"{firstName} {lastName}";
            Address = address;
        }
    }
}
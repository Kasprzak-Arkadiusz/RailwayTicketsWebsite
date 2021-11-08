using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        public override string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool RememberMe { get; set; }
    }

    public class UserRole : IdentityRole
    {
    }
}
using Application.Common.Interfaces;
using Application.Common.Models;
using Infrastructure.Identity.Enums;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentitySeed
    {
        private readonly IIdentityService _identityService;

        public IdentitySeed(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task Seed()
        {
            await CreateAdminAccount();
            await CreateEmployeeAccount();
            await CreateUserAccount();
        }

        private async Task CreateAdminAccount()
        {
            var user = new ApplicationUserParams
            {
                FirstName = "John",
                LastName = "Foe",
                UserName = "superAdmin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userId = await _identityService.CreateUserAsync(user, "sYd&jDc@VU5ZVFn!");

            await _identityService.EnsureUserIsInRoleAsync(userId, Role.Admin.ToString());
            await _identityService.EnsureUserIsInRoleAsync(userId, Role.Employee.ToString());
            await _identityService.EnsureUserIsInRoleAsync(userId, Role.User.ToString());
        }

        private async Task CreateEmployeeAccount()
        {
            var user = new ApplicationUserParams
            {
                FirstName = "Emily",
                LastName = "Smith",
                UserName = "bestEmployee",
                Email = "bestEmployee@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userId = await _identityService.CreateUserAsync(user, "4YDaZf@onq^7FxqH");
            await _identityService.EnsureUserIsInRoleAsync(userId, Role.Employee.ToString());
        }

        private async Task CreateUserAccount()
        {
            var user = new ApplicationUserParams
            {
                FirstName = "William",
                LastName = "Johnson",
                UserName = "justAnUser",
                Email = "user@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userId = await _identityService.CreateUserAsync(user, "7&HjLDhW!ikDhQHJ");
            await _identityService.EnsureUserIsInRoleAsync(userId, Role.User.ToString());
        }
    }
}
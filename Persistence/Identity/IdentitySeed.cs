using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentitySeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeed(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            await CreateAdminAccount();
            await CreateEmployeeAccount();
            await CreateUserAccount();
        }

        private async Task CreateAdminAccount()
        {
            var user = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Foe",
                UserName = "superAdmin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userId = await CreateUser(user, "sYd&jDc@VU5ZVFn!");

            await EnsureRole(userId, Role.Admin.ToString());
            await EnsureRole(userId, Role.Employee.ToString());
            await EnsureRole(userId, Role.User.ToString());
        }

        private async Task CreateEmployeeAccount()
        {
            var user = new ApplicationUser
            {
                FirstName = "Emily",
                LastName = "Smith",
                UserName = "bestEmployee",
                Email = "bestEmployee@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userId = await CreateUser(user, "4YDaZf@onq^7FxqH");

            await EnsureRole(userId, Role.Employee.ToString());
        }

        private async Task CreateUserAccount()
        {
            var user = new ApplicationUser
            {
                FirstName = "William",
                LastName = "Johnson",
                UserName = "justAnUser",
                Email = "user@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userId = await CreateUser(user, "7&HjLDhW!ikDhQHJ");

            await EnsureRole(userId, Role.User.ToString());
        }

        private async Task<string> CreateUser(ApplicationUser user, string password)
        {
            var userToFind = await _userManager.FindByNameAsync(user.UserName);

            if (userToFind == null)
            {
                userToFind = user;
                await _userManager.CreateAsync(userToFind, password);
            }

            if (userToFind == null)
            {
                throw new Exception("User couldn't be created.");
            }

            return userToFind.Id;
        }

        private async Task EnsureRole(string userId, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("Couldn't find a user with provided id.");
            }

            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
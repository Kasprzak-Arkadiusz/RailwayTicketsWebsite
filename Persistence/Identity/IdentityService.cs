using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<string> GetUserEmailAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.Email;
        }

        public async Task<string> GetUserIdByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user.Id;
        }

        public async Task<Result> EnsureUserIsInRoleAsync(string userId, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Result.Failure(new[] { "Couldn't find a user with provided id." });

            if (!await _userManager.IsInRoleAsync(user, role))
                await _userManager.AddToRoleAsync(user, role);

            return Result.Success();
        }

        public async Task<string> CreateUserAsync(ApplicationUserParams userParams, string password)
        {
            var userToFind = await _userManager.FindByEmailAsync(userParams.Email);

            if (userToFind == null)
            {
                Log.Information($"User \"{userParams.UserName} \" created a new account with password.");

                var identity = new ClaimsIdentity(OAuthDefaults.DisplayName);
                identity.AddClaim(new Claim(ClaimTypes.Name, userParams.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, userParams.UserName));

                userToFind = _mapper.Map<ApplicationUser>(userParams);
                await _userManager.CreateAsync(userToFind, password);
            }

            if (userToFind == null)
                throw new Exception("User couldn't be created.");

            return userToFind.Id;
        }
    }
}
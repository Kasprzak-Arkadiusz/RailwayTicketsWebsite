using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserParams userParams, string password)
        {
            var user = new ApplicationUser
            {
                FirstName = userParams.FirstName,
                LastName = userParams.LastName,
                Email = userParams.Email,
                UserName = userParams.UserName
            };

            var result = await _userManager.CreateAsync(user, password);
            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserParams userParams)
        {
            var user = new ApplicationUser
            {
                FirstName = userParams.FirstName,
                LastName = userParams.LastName,
                Email = userParams.Email,
                UserName = userParams.UserName
            };

            var result = await _userManager.CreateAsync(user);
            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<IList<AuthenticationScheme>> GetExternalLogins()
        {
            return (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        }

        public async Task<string> SignInUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            await _signInManager.SignInAsync(user, false);
            return userId;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public bool SignInRequireConfirmedAccount()
        {
            return _userManager.Options.SignIn.RequireConfirmedAccount;
        }

        public async Task<Result> PasswordSignInAsync(string userName, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
            return result.ToApplicationResult();
        }

        public async Task<string> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user.Id;
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<object> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<Result> ExternalLoginSignInAsync(string loginProvider, string providerKey)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, false, true);
            return result.ToApplicationResult();
        }

        public async Task<(Result Result, string UserId)> AddLoginAsync(ApplicationUserParams userParams, object info)
        {
            var user = new ApplicationUser
            {
                FirstName = userParams.FirstName,
                LastName = userParams.LastName,
                Email = userParams.Email,
                UserName = userParams.UserName
            };
            var result = await _userManager.AddLoginAsync(user, (UserLoginInfo) info);

            return (result.ToApplicationResult(), user.Id);
        }
    }
}
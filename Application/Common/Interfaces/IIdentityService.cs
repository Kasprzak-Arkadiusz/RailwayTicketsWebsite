using Application.Common.Models;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserParams useParams, string password);

        Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserParams userParams);

        Task<Result> DeleteUserAsync(string userId);

        Task<IList<AuthenticationScheme>> GetExternalLogins();

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);

        Task<string> SignInUserAsync(string userId);

        Task SignOutUserAsync();

        public bool SignInRequireConfirmedAccount();

        Task<Result> PasswordSignInAsync(string userName, string password, bool rememberMe);

        Task<string> FindByEmailAsync(string email);

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);

        Task<object> GetExternalLoginInfoAsync();

        Task<Result> ExternalLoginSignInAsync(string loginProvider, string providerKey);
        Task<(Result Result, string UserId)> AddLoginAsync(ApplicationUserParams user, object info);
    }
}
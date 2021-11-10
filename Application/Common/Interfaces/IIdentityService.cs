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

        Task<Result> DeleteUserAsync(string userId);

        Task<IList<AuthenticationScheme>> GetExternalLogins();

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);

        Task<string> SignInUserAsync(string userId);

        public bool SignInRequireConfirmedAccount();
    }
}
using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<string> GetUserIdByUserName(string userName);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<Result> EnsureUserIsInRoleAsync(string userId, string role);

        Task<string> CreateUserAsync(ApplicationUserParams user, string password);
    }
}
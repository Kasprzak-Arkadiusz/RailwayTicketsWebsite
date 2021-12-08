using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserEmailAsync(string userId);
        Task<string> GetUserIdByUserName(string userName);

        Task<Result> EnsureUserIsInRoleAsync(string userId, string role);

        Task<string> CreateUserAsync(ApplicationUserParams user, string password);

    }
}
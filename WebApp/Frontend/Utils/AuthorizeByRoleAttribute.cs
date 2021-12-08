using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Frontend.Utils
{
    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeByRoleAttribute(params Role[] roles)
        {
            var stringRoles = string.Join(",", roles);
            Roles = stringRoles;
        }
    }
}
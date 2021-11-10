using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Infrastructure.Identity
{
    public static class SignInResultExtension
    {
        public static Result ToApplicationResult(this SignInResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(new List<string> { "Invalid login attempt." });
        }
    }
}
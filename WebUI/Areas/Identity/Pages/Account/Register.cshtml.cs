using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            IIdentityService identityService,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _identityService = identityService;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First name")]
            [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Username")]
            [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = await _identityService.GetExternalLogins();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ExternalLogins = await _identityService.GetExternalLogins();

            if (!ModelState.IsValid)
                return Page();

            var user = new ApplicationUserParams
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Email = Input.Email,
                UserName = Input.UserName,
            };

            var result = await _identityService.CreateUserAsync(user, Input.Password);
            if (result.Result.Succeeded)
            {
                _logger.LogInformation($"User \"{user.UserName}\" created a new account with password.");

                var userId = result.UserId;
                var code = _identityService.GenerateEmailConfirmationTokenAsync(userId);
                returnUrl ??= Url.Content("~/");
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    null,
                    new { area = "Identity", userId = userId, code, returnUrl },
                    Request.Scheme);

                const string subject = "Confirm your email";
                var htmlMessage = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                await _emailSender.SendEmailAsync(Input.Email, subject, htmlMessage);

                if (_identityService.SignInRequireConfirmedAccount())
                {
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                }

                await _identityService.SignInUserAsync(userId);
                return LocalRedirect(returnUrl);
            }
            foreach (var error in result.Result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
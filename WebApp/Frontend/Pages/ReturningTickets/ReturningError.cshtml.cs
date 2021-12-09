using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Frontend.Utils;

namespace WebApp.Frontend.Pages.ReturningTickets
{
    [AuthorizeByRole(Role.User)]
    public class ReturningErrorModel : PageModel
    {
        public string Error { get; private set; }

        public void OnGet(string error)
        {
            Error = error;
        }
    }
}
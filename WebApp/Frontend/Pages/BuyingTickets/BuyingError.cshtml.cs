using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Frontend.Utils;

namespace WebApp.Frontend.Pages.BuyingTickets
{
    [AuthorizeByRole(Role.User, Role.Admin)]
    public class BuyingErrorModel : PageModel
    {
        public string Error { get; private set; }

        public void OnGet(string error)
        {
            Error = error;
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Frontend.Pages.ReturningTickets
{
    [Authorize]
    public class ReturningErrorModel : PageModel
    {
        public string Error { get; private set; }

        public void OnGet(string error)
        {
            Error = error;
        }
    }
}
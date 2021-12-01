using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Frontend.Pages.Tickets
{
    public class BuyingErrorModel : PageModel
    {
        public string Error { get; private set; }

        public void OnGet(string error)
        {
            Error = error;
        }
    }
}
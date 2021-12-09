using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.ViewReturnedTickets
{
    [AuthorizeByRole(Role.Employee)]
    public class DetailsModel : BasePageModel
    {
        [BindProperty]
        public ReturnedTicketViewModel ReturnedTicket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"ReturnedTicket/{id}";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
                ReturnedTicket = await httpResponseMessage.Content.ReadFromJsonAsync<ReturnedTicketViewModel>();

            if (ReturnedTicket == null)
                return NotFound();

            return Page();
        }
    }
}
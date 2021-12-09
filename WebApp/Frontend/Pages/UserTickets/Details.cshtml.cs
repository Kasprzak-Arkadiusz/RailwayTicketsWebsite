using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.UserTickets
{
    [AuthorizeByRole(Role.Admin, Role.User)]
    public class DetailsModel : BasePageModel
    {
        public TicketViewModel UserTicket { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"Ticket/{id}";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
                UserTicket = await httpResponseMessage.Content.ReadFromJsonAsync<TicketViewModel>();

            if (UserTicket == null)
                return NotFound();

            return Page();
        }
    }
}
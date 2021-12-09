using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.UserTickets
{
    [AuthorizeByRole(Role.User)]
    public class UserTicketsModel : BasePageModel
    {
        public IEnumerable<TicketViewModel> Tickets { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var client = HttpClientFactory.CreateClient("api");

            var userTicketsPath = $"Ticket/userTickets/{currentUserId}";
            var httpResponseMessage = await client.GetAsync(userTicketsPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Tickets = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<TicketViewModel>>();
            }

            return Page();

            //TODO Add details page
        }
    }
}
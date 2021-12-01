using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.UserTickets
{
    public class UserTicketsModel : BasePageModel
    {
        public IEnumerable<TicketDto> Tickets { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //Get current user id
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //Send request for his tickets
            var client = HttpClientFactory.CreateClient("api");

            var userTicketsPath = $"Ticket/userTickets/{currentUserId}";
            var httpResponseMessage = await client.GetAsync(userTicketsPath);

            //Add his tickets to viewModel
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Tickets = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<TicketDto>>();
            }

            return Page();
        }
    }
}
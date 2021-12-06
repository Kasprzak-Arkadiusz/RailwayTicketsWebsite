using Application.Common.DTOs;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.ViewReturnedTickets
{
    public class ViewReturnedTicketsModel : BasePageModel
    {
        public IEnumerable<ReturnedTicketDto> ReturnedTickets { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var client = HttpClientFactory.CreateClient("api");

            const string returnedTicketsPath = "ReturnedTicket";
            var httpResponseMessage = await client.GetAsync(returnedTicketsPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                ReturnedTickets = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ReturnedTicketDto>>();
            }

            return Page();
        }
    }
}
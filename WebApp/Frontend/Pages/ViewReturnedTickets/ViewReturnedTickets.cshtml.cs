using Application.Common.DTOs;
using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Backend.Middleware.Authorization;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.ViewReturnedTickets
{
    [AuthorizeByRole(Role.Employee, Role.Admin)]
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
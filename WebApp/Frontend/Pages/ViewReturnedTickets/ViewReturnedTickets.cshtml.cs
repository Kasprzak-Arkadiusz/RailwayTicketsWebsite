using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.ViewReturnedTickets
{
    [AuthorizeByRole(Role.Employee)]
    public class ViewReturnedTicketsModel : BasePageModel
    {
        public IEnumerable<ReturnedTicketViewModel> ReturnedTickets { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var client = HttpClientFactory.CreateClient("api");

            const string returnedTicketsPath = "ReturnedTicket";
            var httpResponseMessage = await client.GetAsync(returnedTicketsPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                ReturnedTickets = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ReturnedTicketViewModel>>();
            }

            return Page();
        }
    }
}
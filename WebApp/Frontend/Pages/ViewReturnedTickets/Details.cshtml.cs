using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.ViewReturnedTickets
{
    public class DetailsModel : BasePageModel
    {
        [BindProperty]
        public ReturnedTicketDto ReturnedTicket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"ReturnedTicket/{id}";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
                ReturnedTicket = await httpResponseMessage.Content.ReadFromJsonAsync<ReturnedTicketDto>();

            if (ReturnedTicket == null)
                return NotFound();

            return Page();
        }
    }
}
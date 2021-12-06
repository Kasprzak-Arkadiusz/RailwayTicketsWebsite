using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApp.Backend.Models;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.BuyingTickets
{
    public class BuyTicketModel : BasePageModel
    {
        [BindProperty]
        public DisplayTicketViewModel Ticket { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"Ticket/display/{id}";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Ticket = await httpResponseMessage.Content.ReadFromJsonAsync<DisplayTicketViewModel>();
                return Page();
            }

            var error = await httpResponseMessage.Content.ReadAsStringAsync();
            return RedirectToPage("/Tickets/BuyingError", new { error });
        }

        public async Task<IActionResult> OnPost(int trainId, int routeId, int seatReservationId)
        {
            var client = HttpClientFactory.CreateClient("api");
            const string actionPath = "Ticket";

            var requestBody = new CreateTicketRequestBody
            {
                OwnerId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                RouteId = routeId,
                TrainId = (short)trainId,
                SeatReservationId = seatReservationId
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PostAsync(actionPath, content);

            if (httpResponseMessage.IsSuccessStatusCode)
                return RedirectToPage("./BuyingSuccess");

            var postResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            return RedirectToPage("./BuyingError", new { postResponse });
        }
    }
}
using Application.Tickets.Commands.CreateTicket;
using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.BuyingTickets
{
    [AuthorizeByRole(Role.User)]
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

            var command = new CreateTicketCommand
            {
                OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                RouteId = routeId,
                TrainId = (short)trainId,
                SeatReservationId = seatReservationId
            };

            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PostAsync(actionPath, content);

            if (httpResponseMessage.IsSuccessStatusCode)
                return RedirectToPage("./BuyingSuccess");

            var postResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            return RedirectToPage("./BuyingError", new { postResponse });
        }

        public async Task<IActionResult> OnPostCancel(int seatReservationId, int routeId)
        {
            var client = HttpClientFactory.CreateClient("api");
            const string actionPath = "SeatReservation";

            var uriBuilder = new UriBuilder(client.BaseAddress + actionPath);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["routeId"] = routeId.ToString();
            query["id"] = seatReservationId.ToString();

            uriBuilder.Query = query.ToString() ?? string.Empty;
            var url = uriBuilder.ToString();

            var httpResponseMessage = await client.DeleteAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
                return RedirectToPage("/Routes/FindRoutes");

            var postResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            return RedirectToPage("./BuyingError", new { postResponse });
        }
    }
}
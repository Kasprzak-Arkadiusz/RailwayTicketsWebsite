using Application.ReturnedTickets.Commands.CreateReturnedTicket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.ReturningTickets
{
    [Authorize]
    public class ReturnTicketModel : BasePageModel
    {
        [BindProperty]
        public ReturningTicketViewModel Ticket { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            var client = HttpClientFactory.CreateClient("api");
            var ticketPath = $"Ticket/{id}";
            var ticketResponseMessage = await client.GetAsync(ticketPath);

            if (!ticketResponseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            Ticket = await ticketResponseMessage.Content.ReadFromJsonAsync<ReturningTicketViewModel>();
            var reasons = Application.Common.Constants.GenericReasonsOfReturn.ReasonsList;
            Ticket.GenericReasonsOfReturn = DropdownFiller.FillReasonsDropdown(reasons);

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return await OnGet(id);
            }

            var client = HttpClientFactory.CreateClient("api");
            const string returnedTicketPath = "ReturnedTicket";

            var body = new CreateReturnedTicketCommand
            {
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                GenericReasonOfReturn = Ticket.GenericReasonOfReturn,
                PersonalReasonOfReturn = Ticket.PersonalReasonOfReturn,
                TicketId = id
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var ticketResponseMessage = await client.PostAsync(returnedTicketPath, content);

            if (ticketResponseMessage.IsSuccessStatusCode)
                return RedirectToPage("/UserTickets/UserTickets");

            var postResponse = await ticketResponseMessage.Content.ReadAsStringAsync();
            return RedirectToPage("./BuyingError", new { postResponse });
        }
    }
}
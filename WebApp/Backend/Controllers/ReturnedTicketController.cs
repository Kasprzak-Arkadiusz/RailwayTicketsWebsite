using Application.ReturnedTickets.Commands.CreateReturnedTicket;
using Application.Tickets.Commands.DeleteTicket;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Backend.Controllers
{
    public class ReturnedTicketController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateReturnTicket([FromBody] CreateReturnedTicketCommand command, CancellationToken cancellationToken)
        {
            //Create new returnTicket
            await Mediator.Send(command, cancellationToken);

            //Remove old ticket
            await Mediator.Send(new DeleteTicketCommand { Id = command.TicketId }, cancellationToken);

            return Ok();
        }
    }
}
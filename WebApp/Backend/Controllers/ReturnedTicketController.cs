using System.Collections.Generic;
using Application.ReturnedTickets.Commands.CreateReturnedTicket;
using Application.ReturnedTickets.Queries;
using Application.Tickets.Commands.DeleteTicket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Frontend.ViewModels;

namespace WebApp.Backend.Controllers
{
    public class ReturnedTicketController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllReturnedTicketsQuery(), cancellationToken);

            var returnedTickets = Mapper.Map<IEnumerable<ReturnedTicketViewModel>>(result);

            return Ok(returnedTickets);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetReturnedTicketByIdQuery { Id = id }, cancellationToken);

            if (result is null)
                return NotFound("Requested returned ticket could not be found.");

            var returnedTicket = Mapper.Map<ReturnedTicketViewModel>(result);

            return Ok(returnedTicket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReturnTicket([FromBody] CreateReturnedTicketCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            await Mediator.Send(new DeleteTicketCommand { Id = command.TicketId }, cancellationToken);

            return Ok();
        }
    }
}
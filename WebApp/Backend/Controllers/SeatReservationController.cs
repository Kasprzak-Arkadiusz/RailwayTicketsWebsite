using Application.Routes.Commands.UpdateRoute;
using Application.Routes.Queries;
using Application.SeatReservations.Commands.DeleteSeatReservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Backend.Controllers
{
    public class SeatReservationController : BaseApiController
    {
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReservation(int id, int routeId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteSeatReservation { Id = id }, cancellationToken);

            var routeDto = await Mediator.Send(new GetRouteByIdQuery { Id = routeId }, cancellationToken);

            var updateRouteCommand = Mapper.Map<UpdateRouteCommand>(routeDto);
            updateRouteCommand.NumberOfFreeSeats++;
            await Mediator.Send(updateRouteCommand, cancellationToken);

            return Ok();
        }
    }
} 
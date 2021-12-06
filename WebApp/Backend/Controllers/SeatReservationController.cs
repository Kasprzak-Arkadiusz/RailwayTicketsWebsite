using Application.SeatReservations.Commands.DeleteSeatReservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Backend.Controllers
{
    public class SeatReservationController : BaseApiController
    {
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteReservation(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteSeatReservation { Id = id }, cancellationToken);

            return Ok();
        }
    }
}
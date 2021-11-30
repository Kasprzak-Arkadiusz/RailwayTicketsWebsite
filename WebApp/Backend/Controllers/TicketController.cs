using Application.Common.DTOs;
using Application.Routes.Queries;
using Application.Seats.Commands.CreateSeat;
using Application.Seats.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp.Backend.Controllers
{
    public class TicketController : BaseApiController
    {
        [HttpGet("display/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDisplayDataById(int id, CancellationToken cancellationToken)
        {
            //Get the current user email address
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (userEmail is null)
            {
                return Unauthorized("We couldn't find your email address.");
            }

            //Get the selected route
            var routeDto = await Mediator.Send(new GetRouteByIdQuery { Id = id }, cancellationToken);

            if (routeDto is null)
            {
                return BadRequest("Requested route couldn't be found.");
            }

            //Check if train has free seats
            var seat = await Mediator.Send(new GetAnySeatQuery { TrainId = routeDto.TrainId }, cancellationToken) ??
                       await Mediator.Send(new CreateSeatCommand { TrainId = routeDto.TrainId }, cancellationToken);

            return Ok(new TicketDto
            {
                Email = userEmail,
                DepartureTime = routeDto.DepartureTime,
                ArrivalTime = routeDto.ArrivalTime,
                StartingStation = routeDto.StartingStation,
                FinalStation = routeDto.FinalStation,
                TrainId = routeDto.TrainId,
                Car = seat.Car,
                SeatNumber = seat.Number
            });
        }
    }
}
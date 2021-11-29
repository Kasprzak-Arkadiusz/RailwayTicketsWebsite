using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Routes.Queries;
using Application.Seats.Commands.CreateSeat;
using Application.Seats.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Backend.Controllers
{
    public class TicketController : BaseApiController
    {
        [HttpGet("display/{id}")]
        public async Task<IActionResult> GetDisplayDataById(int id, CancellationToken cancellationToken)
        {
            //Get the current user email address (this doesn't work)
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            //Get the selected route
            var routeDto = await Mediator.Send(new GetRouteByIdQuery { Id = id }, cancellationToken);

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

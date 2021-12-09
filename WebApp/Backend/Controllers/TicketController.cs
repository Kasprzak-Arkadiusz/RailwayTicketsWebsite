using Application.Routes.Queries;
using Application.SeatReservations.Commands.CreateSeatReservation;
using Application.Seats.Commands.CreateSeat;
using Application.Seats.Queries;
using Application.Tickets.Commands.CreateTicket;
using Application.Tickets.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Frontend.ViewModels;

namespace WebApp.Backend.Controllers
{
    public class TicketController : BaseApiController
    {
        [HttpGet("display/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDisplayDataById(int id, CancellationToken cancellationToken)
        {
            //Get the selected route
            var routeDto = await Mediator.Send(new GetRouteByIdQuery { Id = id }, cancellationToken);

            if (routeDto is null)
            {
                return BadRequest("Requested route couldn't be found.");
            }

            //Check if train has free seats
            var seatDto = await Mediator.Send(new GetAnySeatQuery { TrainId = routeDto.TrainId }, cancellationToken) ??
                       await Mediator.Send(new CreateSeatCommand { TrainId = routeDto.TrainId }, cancellationToken);

            //Pre-book a seat
            var seatReservationId = await Mediator.Send(new CreateSeatReservationCommand { SeatId = seatDto.Id }, cancellationToken);

            var displayTicketViewModel = new DisplayTicketViewModel
            {
                DepartureTime = routeDto.DepartureTime,
                ArrivalTime = routeDto.ArrivalTime,
                StartingStation = routeDto.StartingStation,
                FinalStation = routeDto.FinalStation,
                TrainIdentifier = routeDto.TrainId,
                Car = seatDto.Car,
                SeatNumber = seatDto.Number,
                TrainId = routeDto.TrainId,
                RouteId = routeDto.Id,
                SeatReservationId = seatReservationId
            };

            return Ok(displayTicketViewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTicketById(int id, CancellationToken cancellationToken)
        {
            var ticket = await Mediator.Send(new GetTicketByIdQuery { Id = id }, cancellationToken);

            if (ticket is null)
            {
                return NotFound("Your ticket could not be found.");
            }
            var returnTicketViewModel = Mapper.Map<ReturningTicketViewModel>(ticket);

             return Ok(returnTicketViewModel);
        }

        [HttpGet("userTickets/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserTicketsByUserId(string id, CancellationToken cancellationToken)
        {
            var userTickets = await Mediator.Send(new GetUserTicketsQuery { UserId = id }, cancellationToken);

            var returnTicketViewModel = Mapper.Map<IEnumerable<TicketViewModel>>(userTickets);

            return Ok(returnTicketViewModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command, CancellationToken cancellationToken)
        {
            var ticketId = await Mediator.Send(command, cancellationToken);

            return Ok(ticketId);
        }
    }
}
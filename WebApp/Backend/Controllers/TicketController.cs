﻿using Application.Common.DTOs;
using Application.Routes.Queries;
using Application.SeatReservations.Commands;
using Application.Seats.Commands.CreateSeat;
using Application.Seats.Queries;
using Application.Tickets.Commands.CreateTicket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Application.Tickets.Queries;
using WebApp.Backend.Models;
using WebApp.Frontend.Utils;

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
            //TODO When user resign free a seat
            var seatReservationId = await Mediator.Send(new CreateSeatReservationCommand { SeatId = seatDto.Id }, cancellationToken);

            return Ok(new DisplayTicketViewModel
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
            });
        }

        [HttpGet("userTickets/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserTicketsById(string id, CancellationToken cancellationToken)
        {
            var userTickets = await Mediator.Send(new GetUserTicketsQuery { UserId = id }, cancellationToken);

            return Ok(userTickets);
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
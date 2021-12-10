using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Queries
{
    public class GetUserTicketsQueryHandler : IQueryHandler<GetUserTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetUserTicketsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketDto>> Handle(GetUserTicketsQuery request, CancellationToken cancellationToken)
        {
            var userHasTickets = _context.Tickets.Where(t => t.OwnerId == request.UserId).Any(t => t.SeatReservations != null);

            if (!userHasTickets)
            {
                return new List<TicketDto>();
            }

            var userTickets = await _context.Tickets.Where(t => t.OwnerId == request.UserId && t.SeatReservations != null)
                .Select(ticket => new
                {
                    ticket.Id,
                    StartingStation = ticket.Route.StartingStation.Name,
                    FinalStation = ticket.Route.FinalStation.Name,
                    ticket.Route.DepartureTime,
                    ticket.Route.ArrivalTime,
                    ticket.Train.TrainId,
                    Car = ticket.SeatReservations.Select(sr => sr.Seat.Car).FirstOrDefault(),
                    SeatNumber = ticket.SeatReservations.Select(sr => sr.Seat.Number).FirstOrDefault(),
                }).ToListAsync(cancellationToken);

            var userTicketsDto = userTickets.Select(ticket => new TicketDto
            {
                Id = ticket.Id,
                StartingStation = ticket.StartingStation,
                FinalStation = ticket.FinalStation,
                DepartureTime = ticket.DepartureTime,
                ArrivalTime = ticket.ArrivalTime,
                TrainIdentifier = ticket.TrainId,
                Car = ticket.Car,
                SeatNumber = ticket.SeatNumber
            }).ToList();

            return userTicketsDto.AsReadOnly();
        }
    }
}
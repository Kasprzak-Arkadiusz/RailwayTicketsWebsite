using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Queries
{
    public class GetTicketByIdQueryHandler : IQueryHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly IApplicationDbContext _context;

        public GetTicketByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.Where(t => t.Id == request.Id).Select(ticket => new
            {
                ticket.Id,
                StartingStation = ticket.Route.StartingStation.Name,
                FinalStation = ticket.Route.FinalStation.Name,
                ticket.Route.DepartureTime,
                ticket.Route.ArrivalTime,
                ticket.Train.TrainId,
                Car = ticket.SeatReservations.Select(sr => sr.Seat.Car).First(),
                SeatNumber = ticket.SeatReservations.Select(sr => sr.Seat.Number).First(),
            }).FirstAsync(cancellationToken);

            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                StartingStation = ticket.StartingStation,
                FinalStation = ticket.FinalStation,
                DepartureTime = ticket.DepartureTime,
                ArrivalTime = ticket.ArrivalTime,
                TrainIdentifier = ticket.TrainId,
                Car = ticket.Car,
                SeatNumber = ticket.SeatNumber
            };

            return ticketDto;
        }
    }
}
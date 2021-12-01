using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var route = await _context.Routes.FindAsync(new object[] { request.RouteId }, cancellationToken);

            var train = await _context.Trains.FirstOrDefaultAsync(t => t.TrainId == request.TrainId, cancellationToken);

            var seatReservation = await _context.SeatReservations.FindAsync(new object[] { request.SeatReservationId }, cancellationToken);

            var ticket = new Ticket
            {
                OwnerId = request.OwnerId,
                Route = route,
                Train = train,
                SeatReservations = new List<SeatReservation>()
            };

            ticket.SeatReservations.Add(seatReservation);

            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync();

            return ticket.Id;
        }
    }
}
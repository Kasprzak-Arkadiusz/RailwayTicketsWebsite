using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReturnedTickets.Commands.CreateReturnedTicket
{
    public class CreateReturnedTicketCommandHandler : ICommandHandler<CreateReturnedTicketCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public CreateReturnedTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateReturnedTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToReturn = _context.Tickets.Include(t => t.Route).ThenInclude(r => r.FinalStation)
                .Include(t => t.Route).ThenInclude(r => r.StartingStation)
                .Include(t => t.Train)
                .First(t => t.Id == request.TicketId);

            var seatReservation = await _context.SeatReservations.Select(sr => new
            {
                sr.Seat,
                TicketId = sr.Ticket.Id
            }).Where(sr => sr.TicketId == request.TicketId).FirstOrDefaultAsync(cancellationToken);

            var returnedTicket = new ReturnedTicket(request.Email, request.GenericReasonOfReturn,
                request.PersonalReasonOfReturn, ticketToReturn, seatReservation.Seat);

            await _context.ReturnedTickets.AddAsync(returnedTicket, cancellationToken);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
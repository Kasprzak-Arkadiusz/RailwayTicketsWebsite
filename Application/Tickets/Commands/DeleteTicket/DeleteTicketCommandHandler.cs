using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler : ICommandHandler<DeleteTicketCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToDelete = await _context.Tickets.FindAsync(new object[] { request.Id }, cancellationToken);

            if (ticketToDelete is null)
            {
                throw new NotFoundException("Ticket could not be found.");
            }

            _context.Tickets.Remove(ticketToDelete);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
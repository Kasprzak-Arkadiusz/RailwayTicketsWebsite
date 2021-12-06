using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.SeatReservations.Commands.DeleteSeatReservation
{
    public class DeleteSeatReservationHandler : IRequestHandler<DeleteSeatReservation, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSeatReservationHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSeatReservation request, CancellationToken cancellationToken)
        {
            var seatReservationToDelete = await _context.SeatReservations.FindAsync(request.Id);

            if (seatReservationToDelete is null)
            {
                throw new NotFoundException("Seat reservation could not be found.");
            }

            _context.SeatReservations.Remove(seatReservationToDelete);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SeatReservations.Commands
{
    public class CreateSeatReservationCommandHandler : IRequestHandler<CreateSeatReservationCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateSeatReservationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSeatReservationCommand request, CancellationToken cancellationToken)
        {
            var seat = await _context.Seats.FindAsync(new object[] { request.SeatId }, cancellationToken);

            var seatReservation = new SeatReservation(seat);

            await _context.SeatReservations.AddAsync(seatReservation, cancellationToken);
            await _context.SaveChangesAsync();

            return seatReservation.Id;
        }
    }
}
using MediatR;

namespace Application.SeatReservations.Commands
{
    public class CreateSeatReservationCommand : IRequest<int>
    {
        public int SeatId { get; set; }
    }
}
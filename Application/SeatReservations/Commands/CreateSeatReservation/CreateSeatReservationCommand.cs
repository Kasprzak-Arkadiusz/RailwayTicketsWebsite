using MediatR;

namespace Application.SeatReservations.Commands.CreateSeatReservation
{
    public class CreateSeatReservationCommand : IRequest<int>
    {
        public int SeatId { get; set; }
    }
}
using MediatR;

namespace Application.SeatReservations.Commands.DeleteSeatReservation
{
    public class DeleteSeatReservation : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
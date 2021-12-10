using Application.Abstractions.Messaging;

namespace Application.SeatReservations.Commands.CreateSeatReservation
{
    public class CreateSeatReservationCommand : ICommand<int>
    {
        public int SeatId { get; set; }
    }
}
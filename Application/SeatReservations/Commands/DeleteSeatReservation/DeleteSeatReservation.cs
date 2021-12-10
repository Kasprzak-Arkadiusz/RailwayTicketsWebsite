using Application.Abstractions.Messaging;
using MediatR;

namespace Application.SeatReservations.Commands.DeleteSeatReservation
{
    public class DeleteSeatReservation : ICommand<Unit>
    {
        public int Id { get; set; }
    }
}
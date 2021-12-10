using Application.Abstractions.Messaging;

namespace Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommand : ICommand<int>
    {
        public string OwnerId { get; set; }
        public short TrainId { get; set; }
        public int RouteId { get; set; }
        public int SeatReservationId { get; set; }
    }
}
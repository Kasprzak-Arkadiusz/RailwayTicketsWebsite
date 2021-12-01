using MediatR;

namespace Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<int>
    {
        public string OwnerId { get; set; }
        public short TrainId { get; set; }
        public int RouteId { get; set; }
        public int SeatReservationId { get; set; }
    }
}
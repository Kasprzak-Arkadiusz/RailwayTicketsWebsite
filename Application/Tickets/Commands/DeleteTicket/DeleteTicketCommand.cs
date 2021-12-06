using MediatR;

namespace Application.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommand : IRequest
    {
        public int Id { get; set; }
    }
}
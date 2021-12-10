using Application.Abstractions.Messaging;
using MediatR;

namespace Application.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }
}
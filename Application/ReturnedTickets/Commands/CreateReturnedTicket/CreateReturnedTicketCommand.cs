using Application.Abstractions.Messaging;
using MediatR;

namespace Application.ReturnedTickets.Commands.CreateReturnedTicket
{
    public class CreateReturnedTicketCommand : ICommand<Unit>
    {
        public string Email { get; set; }
        public int TicketId { get; set; }
        public string GenericReasonOfReturn { get; set; }
        public string PersonalReasonOfReturn { get; set; }
    }
}
using MediatR;

namespace Application.ReturnedTickets.Commands.CreateReturnedTicket
{
    public class CreateReturnedTicketCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public int TicketId { get; set; }
        public string GenericReasonOfReturn { get; set; }
        public string PersonalReasonOfReturn { get; set; }
    }
}
using FluentValidation;

namespace Application.ReturnedTickets.Commands.CreateReturnedTicket
{
    public class CreateReturnedTicketCommandValidator : AbstractValidator<CreateReturnedTicketCommand>
    {
        public CreateReturnedTicketCommandValidator()
        {
            RuleFor(x => x.GenericReasonOfReturn).NotEmpty();
            RuleFor(x => x.PersonalReasonOfReturn).NotEmpty();
        }
    }
}
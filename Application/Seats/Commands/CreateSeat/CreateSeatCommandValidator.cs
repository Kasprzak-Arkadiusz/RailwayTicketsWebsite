using FluentValidation;

namespace Application.Seats.Commands.CreateSeat
{
    public class CreateSeatCommandValidator : AbstractValidator<CreateSeatCommand>
    {
        public CreateSeatCommandValidator()
        {
            RuleFor(s => s.TrainId).NotNull().GreaterThan(0);
        }
    }
}
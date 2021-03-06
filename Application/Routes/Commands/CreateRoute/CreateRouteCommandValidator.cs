using FluentValidation;

namespace Application.Routes.Commands.CreateRoute
{
    public sealed class CreateRouteCommandValidator : AbstractValidator<CreateRouteCommand>
    {
        public CreateRouteCommandValidator()
        {
            RuleFor(x => x.ArrivalTime).NotEqual(x => x.DepartureTime)
                .WithMessage("The arrival time cannot be equal to the departure time.").NotEmpty();
            RuleFor(x => x.FinalStation).NotEqual(x => x.StartingStation)
                .WithMessage("The final station cannot be the starting station at the same time").NotEmpty();
            RuleFor(x => x.DepartureTime).NotEmpty();
            RuleFor(x => x.StartingStation).NotEmpty();
            RuleFor(x => x.TrainId).NotEmpty();
        }
    }
}
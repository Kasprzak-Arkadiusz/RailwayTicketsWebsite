using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Commands
{
    public class CreateRouteCommand : IRequest<int>
    {
        public int StartingStation { get; set; }
        public int FinalStation { get; set; }
        public short DepartureTimeInMinutesPastMidnight { get; set; }
        public short ArrivalTimeInMinutesPastMidnight { get; set; }
        public bool IsOnHold { get; set; }
    }

    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateRouteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            var entity = new Route
            {
                StartingStation = 1,
                FinalStation = 2,
                DepartureTimeInMinutesPastMidnight = request.DepartureTimeInMinutesPastMidnight,
                ArrivalTimeInMinutesPastMidnight = request.ArrivalTimeInMinutesPastMidnight,
                IsOnHold = request.IsOnHold
            };

            await _context.Routes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
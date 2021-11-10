using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.Routes.Commands
{
    public class UpdateRouteCommand : IRequest
    {
        public int Id { get; set; }
        public short DepartureTimeInMinutesPastMidnight { get; set; }
        public short ArrivalTimeInMinutesPastMidnight { get; set; }
        public bool IsOnHold { get; set; }
    }

    public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRouteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Routes.FindAsync(request.Id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(entity), request.Id);
            }

            entity.DepartureTimeInMinutesPastMidnight = request.DepartureTimeInMinutesPastMidnight;
            entity.ArrivalTimeInMinutesPastMidnight = request.ArrivalTimeInMinutesPastMidnight;
            entity.IsOnHold = request.IsOnHold;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
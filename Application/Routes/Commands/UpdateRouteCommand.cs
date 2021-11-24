using Application.Common.Converters;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Commands
{
    public class UpdateRouteCommand : IRequest
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsOnHold { get; set; }
        public string StartingStationName { get; set; }
        public string FinalStationName { get; set; }
        public short TrainId { get; set; }
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

            entity.DepartureTimeInMinutesPastMidnight = DateTimeToShortConverter.Convert(request.DepartureTime);
            entity.ArrivalTimeInMinutesPastMidnight = DateTimeToShortConverter.Convert(request.ArrivalTime);
            entity.IsOnHold = request.IsOnHold;
            entity.StartingStation = await _context.Stations.SingleAsync(
                s => s.Name == request.StartingStationName, cancellationToken);
            entity.FinalStation = await _context.Stations.SingleAsync(
                s => s.Name == request.FinalStationName, cancellationToken);
            entity.Train = await _context.Trains.SingleAsync(
                t => t.TrainId == request.TrainId, cancellationToken);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
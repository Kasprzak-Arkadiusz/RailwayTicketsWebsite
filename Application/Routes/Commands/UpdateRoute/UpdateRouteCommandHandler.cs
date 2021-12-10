using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Commands.UpdateRoute
{
    public class UpdateRouteCommandHandler : ICommandHandler<UpdateRouteCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRouteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Routes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException("A route couldn't be found");
            }

            var startingStation = await _context.Stations.SingleOrDefaultAsync(
                s => s.Name == request.StartingStation, cancellationToken);
            var finalStation = await _context.Stations.SingleOrDefaultAsync(
                s => s.Name == request.FinalStation, cancellationToken);
            var train = await _context.Trains.SingleOrDefaultAsync(
                t => t.TrainId == request.TrainId, cancellationToken);

            entity.DepartureTime = request.DepartureTime;
            entity.ArrivalTime = request.ArrivalTime;
            entity.IsSuspended = request.IsSuspended;
            entity.NumberOfFreeSeats = request.NumberOfFreeSeats;
            entity.StartingStation = startingStation ?? throw new NotFoundException("A starting station couldn't be found.");
            entity.FinalStation = finalStation ?? throw new NotFoundException("A final station couldn't be found.");
            entity.Train = train ?? throw new NotFoundException("A train couldn't be found.");

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
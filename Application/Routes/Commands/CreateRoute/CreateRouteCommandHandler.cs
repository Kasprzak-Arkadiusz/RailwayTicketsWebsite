using System;
using Application.Common.Converters;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.Routes.Commands.CreateRoute
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateRouteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            var startingStation = await _context.Stations.SingleOrDefaultAsync(
                s => s.Name == request.StartingStation, cancellationToken);
            if (startingStation == null)
            {
                throw new NotFoundException("A starting station couldn't be found.");
            }

            var finalStation = await _context.Stations.SingleOrDefaultAsync(
                s => s.Name == request.FinalStation, cancellationToken);
            if (finalStation == null)
            {
                throw new NotFoundException("A final station couldn't be found.");
            }

            var train = await _context.Trains.SingleOrDefaultAsync(
                t => t.TrainId == request.TrainId, cancellationToken);
            if (train == null)
            {
                throw new NotFoundException("A train couldn't be found.");
            }

            var entity = new Route
            {
                DepartureTimeInMinutesPastMidnight = DateTimeToShortConverter.Convert(request.DepartureTime),
                ArrivalTimeInMinutesPastMidnight = DateTimeToShortConverter.Convert(request.ArrivalTime),
                IsOnHold = request.IsOnHold,
                StartingStation = startingStation,
                FinalStation = finalStation,
                Train = train
            };

            await _context.Routes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
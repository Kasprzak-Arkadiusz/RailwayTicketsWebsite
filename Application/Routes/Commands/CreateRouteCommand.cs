using Application.Common.Converters;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Commands
{
    public class CreateRouteCommand : IRequest<int>
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsOnHold { get; set; }
        public string StartingStationName { get; set; }
        public string FinalStationName { get; set; }
        public short TrainId { get; set; }
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
                DepartureTimeInMinutesPastMidnight = DateTimeToShortConverter.Convert(request.DepartureTime),
                ArrivalTimeInMinutesPastMidnight = DateTimeToShortConverter.Convert(request.ArrivalTime),
                IsOnHold = request.IsOnHold,
                StartingStation = await _context.Stations.SingleAsync(
                    s => s.Name == request.StartingStationName, cancellationToken),
                FinalStation = await _context.Stations.SingleAsync(
                s => s.Name == request.FinalStationName, cancellationToken),
                Train = await _context.Trains.SingleAsync(
                    t => t.TrainId == request.TrainId, cancellationToken)
            };

            await _context.Routes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
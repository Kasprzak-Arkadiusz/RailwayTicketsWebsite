using Application.Common.Converters;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Queries
{
    public class GetAllRoutesQuery : IRequest<IEnumerable<RouteDto>>
    { }

    public class GetAllRoutesQueryHandler : IRequestHandler<GetAllRoutesQuery, IEnumerable<RouteDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllRoutesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RouteDto>> Handle(GetAllRoutesQuery request, CancellationToken cancellationToken)
        {
            var routes = await _context.Routes.Select(route => new RouteDto
            {
                Id = route.Id,
                StartingStation = route.StartingStation.Name,
                FinalStation = route.FinalStation.Name,
                ArrivalTime = ShortToDateTimeConverter.Convert(route.ArrivalTimeInMinutesPastMidnight),
                DepartureTime = ShortToDateTimeConverter.Convert(route.DepartureTimeInMinutesPastMidnight),
                IsOnHold = route.IsOnHold,
                TrainId = route.Train.TrainId
            }).ToListAsync(cancellationToken);

            return routes.AsReadOnly();
        }
    }
}
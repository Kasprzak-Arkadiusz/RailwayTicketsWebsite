using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Queries
{
    public class GetAllRoutesQuery : IQuery<IEnumerable<RouteDto>>
    { }

    public class GetAllRoutesQueryHandler : IQueryHandler<GetAllRoutesQuery, IEnumerable<RouteDto>>
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
                ArrivalTime = route.ArrivalTime,
                DepartureTime = route.DepartureTime,
                IsSuspended = route.IsSuspended,
                TrainId = route.Train.TrainId
            }).Where(r => r.IsSuspended == false).ToListAsync(cancellationToken);

            return routes.AsReadOnly();
        }
    }
}
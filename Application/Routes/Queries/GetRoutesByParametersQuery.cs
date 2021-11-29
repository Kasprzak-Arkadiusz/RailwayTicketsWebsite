using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Queries
{
    public class GetRoutesByParametersQuery : IRequest<IEnumerable<RouteDto>>
    {
        public string StartingStation { get; set; }
        public string FinalStation { get; set; }
        public DateTime DepartureTime { get; set; }
    }

    public class GetRoutesWithConditionsQueryHandler : IRequestHandler<GetRoutesByParametersQuery, IEnumerable<RouteDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetRoutesWithConditionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        //TODO Return only routes that are not suspended
        public async Task<IEnumerable<RouteDto>> Handle(GetRoutesByParametersQuery request, CancellationToken cancellationToken)
        {
            var queryBuilder = _context.Routes.AsQueryable();
            if (request.StartingStation is not null)
            {
                queryBuilder = queryBuilder.Where(r => r.StartingStation.Name == request.StartingStation);
            }

            if (request.FinalStation is not null)
            {
                queryBuilder = queryBuilder.Where(r => r.FinalStation.Name == request.FinalStation);
            }

            if (request.DepartureTime != DateTime.MinValue)
            {
                queryBuilder = queryBuilder.Where(r => r.DepartureTime >= request.DepartureTime);
            }

            var routes = await queryBuilder.Select(route => new RouteDto
            {
                Id = route.Id,
                StartingStation = route.StartingStation.Name,
                FinalStation = route.FinalStation.Name,
                ArrivalTime = route.ArrivalTime,
                DepartureTime = route.DepartureTime,
                IsSuspended = route.IsSuspended,
                TrainId = route.Train.TrainId
            }).ToListAsync(cancellationToken);

            return routes?.AsReadOnly();
        }
    }
}
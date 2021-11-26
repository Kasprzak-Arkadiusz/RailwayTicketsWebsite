using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Converters;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Routes.Queries
{
    public class GetRoutesByParametersQuery : IRequest<IEnumerable<RouteDto>>
    {
        public string StartingStation { get; set; }
        public string FinalStation { get; set;}
        public DateTime DepartureTime { get; set; }
    }

    public class GetRoutesWithConditionsQueryHandler : IRequestHandler<GetRoutesByParametersQuery ,IEnumerable<RouteDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetRoutesWithConditionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        //TODO Return only routes that are not on hold
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
                var departureTimeInMinutes = DateTimeToShortConverter.Convert(request.DepartureTime);
                queryBuilder = queryBuilder.Where(r => r.DepartureTimeInMinutesPastMidnight >= departureTimeInMinutes);
            }

            var routes = await queryBuilder.Select(route => new RouteDto
            {
                Id = route.Id,
                StartingStation = route.StartingStation.Name,
                FinalStation = route.FinalStation.Name,
                ArrivalTime = ShortToDateTimeConverter.Convert(route.ArrivalTimeInMinutesPastMidnight),
                DepartureTime = ShortToDateTimeConverter.Convert(route.DepartureTimeInMinutesPastMidnight),
                IsOnHold = route.IsOnHold,
                TrainId = route.Train.TrainId
            }).ToListAsync(cancellationToken);

            return routes?.AsReadOnly();
        }
    }
}
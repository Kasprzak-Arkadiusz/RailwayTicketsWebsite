using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Converters;

namespace Application.Routes.Queries
{
    public class GetRouteByIdQuery : IRequest<RouteDto>
    {
        public int Id { get; set; }
    }

    public class GetRouteByIdQueryHandler : IRequestHandler<GetRouteByIdQuery, RouteDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRouteByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RouteDto> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Routes
                .Where(r => r.Id == request.Id)
                .Select(route => new RouteDto
                {
                    Id = route.Id,
                    StartingStation = route.StartingStationNavigation.Name,
                    FinalStation = route.FinalStationNavigation.Name,
                    ArrivalTime = ShortToDateTimeConverter.Convert(route.ArrivalTimeInMinutesPastMidnight),
                    DepartureTime = ShortToDateTimeConverter.Convert(route.DepartureTimeInMinutesPastMidnight),
                    IsOnHold = route.IsOnHold
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
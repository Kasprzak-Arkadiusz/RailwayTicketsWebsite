using Application.Common.Converters;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Queries
{
    public class GetRouteByIdQuery : IRequest<RouteDto>
    {
        public int Id { get; set; }
    }

    public class GetRouteByIdQueryHandler : IRequestHandler<GetRouteByIdQuery, RouteDto>
    {
        private readonly IApplicationDbContext _context;

        public GetRouteByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RouteDto> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Routes
                .Where(r => r.Id == request.Id)
                .Select(route => new RouteDto
                {
                    Id = route.Id,
                    StartingStation = route.StartingStation.Name,
                    FinalStation = route.FinalStation.Name,
                    ArrivalTime = route.ArrivalTime,
                    DepartureTime = route.DepartureTime,
                    IsSuspended = route.IsSuspended,
                    TrainId = route.Train.TrainId
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
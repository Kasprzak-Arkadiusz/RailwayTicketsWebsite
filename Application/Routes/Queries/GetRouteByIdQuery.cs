using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Queries
{
    public class GetRouteByIdQuery : IQuery<RouteDto>
    {
        public int Id { get; set; }
    }

    public class GetRouteByIdQueryHandler : IQueryHandler<GetRouteByIdQuery, RouteDto>
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
            var routeDtosWithRequestedId = _mapper.ProjectTo<RouteDto>(_context.Routes.Where(r => r.Id == request.Id));

            var routeDto = await routeDtosWithRequestedId.FirstOrDefaultAsync(cancellationToken);

            return routeDto;
        }
    }
}
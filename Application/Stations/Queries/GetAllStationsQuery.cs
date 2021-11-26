using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stations.Queries
{
    public class GetAllStationsQuery : IRequest<IEnumerable<StationDto>>
    { }

    public class GetAllStationsQueryHandler : IRequestHandler<GetAllStationsQuery, IEnumerable<StationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllStationsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StationDto>> Handle(GetAllStationsQuery request, CancellationToken cancellationToken)
        {
            var stations = await _mapper.ProjectTo<StationDto>(_context.Stations).ToListAsync(cancellationToken);
            return stations.AsReadOnly();
        }
    }
}
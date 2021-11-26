using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Trains.Queries
{
    public class GetAllTrainsQuery : IRequest<IEnumerable<TrainDto>>
    { }

    public class GetAllTrainsQueryHandler : IRequestHandler<GetAllTrainsQuery ,IEnumerable<TrainDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTrainsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainDto>> Handle(GetAllTrainsQuery request, CancellationToken cancellationToken)
        {
            var trains = await _mapper.ProjectTo<TrainDto>(_context.Trains).ToListAsync(cancellationToken);
            return trains.AsReadOnly();
        }
    }
}
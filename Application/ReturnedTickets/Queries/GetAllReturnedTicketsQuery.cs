using Application.Common.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.ReturnedTickets.Queries
{
    public class GetAllReturnedTicketsQuery : IRequest<IEnumerable<ReturnedTicketDto>>
    { }

    public class GetAllReturnedTicketsQueryHandler : IRequestHandler<GetAllReturnedTicketsQuery, IEnumerable<ReturnedTicketDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllReturnedTicketsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReturnedTicketDto>> Handle(GetAllReturnedTicketsQuery request, CancellationToken cancellationToken)
        {
            var returnedTickets = await _context.ReturnedTickets.ProjectTo<ReturnedTicketDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return returnedTickets;
        }
    }
}
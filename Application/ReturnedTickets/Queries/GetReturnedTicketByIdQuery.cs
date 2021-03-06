using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReturnedTickets.Queries
{
    public class GetReturnedTicketByIdQuery : IQuery<ReturnedTicketDto>
    {
        public int Id { get; set; }
    }

    public class GetReturnedTicketByIdQueryHandler : IQueryHandler<GetReturnedTicketByIdQuery, ReturnedTicketDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReturnedTicketByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReturnedTicketDto> Handle(GetReturnedTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var returnedTicket = await _context.ReturnedTickets.FindAsync(new object[] { request.Id }, cancellationToken);

            var returnedTicketDto = _mapper.Map<ReturnedTicketDto>(returnedTicket);

            return returnedTicketDto;
        }
    }
}
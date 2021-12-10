using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seats.Queries
{
    public class GetSeatByIdQuery : IQuery<SeatDto>
    {
        public int Id { get; set; }
    }

    public class GetSeatByIdQueryHandler : IQueryHandler<GetSeatByIdQuery, SeatDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSeatByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeatDto> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
        {
            var seat = await _context.Seats.FindAsync(new object[] { request.Id }, cancellationToken);

            var seatDto = _mapper.Map<SeatDto>(seat);

            return seatDto;
        }
    }
}
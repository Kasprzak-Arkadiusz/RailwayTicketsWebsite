using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Seats.Queries
{
    public class GetSeatByIdQuery : IRequest<SeatDto>
    {
        public int Id { get; set; }
    }

    public class GetSeatByIdQueryHandler : IRequestHandler<GetSeatByIdQuery, SeatDto>
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
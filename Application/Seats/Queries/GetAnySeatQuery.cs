using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Seats.Queries
{
    public class GetAnySeatQuery : IRequest<SeatDto>
    {
        public short TrainId { get; set; }
    }

    public class GetAnySeatQueryHandler : IRequestHandler<GetAnySeatQuery, SeatDto>
    {
        private readonly IApplicationDbContext _context;
        public GetAnySeatQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SeatDto> Handle(GetAnySeatQuery request, CancellationToken cancellationToken)
        {
            var seat = await _context.Seats.Include(s => s.Train)
                .Where(s => s.SeatReservation == null && s.Train.TrainId == request.TrainId).FirstOrDefaultAsync(cancellationToken);

            if (seat is null)
            {
                return null;
            }

            var seatDto = new SeatDto()
            {
                Car = seat.Car,
                Number = seat.Number,
                TrainId = seat.Train.TrainId
            };

            return seatDto;
        }
    }
}
using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seats.Queries
{
    public class GetAnySeatQuery : IQuery<SeatDto>
    {
        public short TrainId { get; set; }
    }

    public class GetAnySeatQueryHandler : IQueryHandler<GetAnySeatQuery, SeatDto>
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
                Id = seat.Id,
                Car = seat.Car,
                Number = seat.Number,
                TrainId = seat.Train.TrainId
            };

            return seatDto;
        }
    }
}
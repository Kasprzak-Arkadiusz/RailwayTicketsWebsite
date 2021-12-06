using Application.Common.DTOs;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.SeatReservations.Queries
{
    public class GetSeatReservationByTicketIdHandler : IRequestHandler<GetSeatReservationByTicketId, SeatReservationDto>
    {
        private readonly IApplicationDbContext _context;
        public GetSeatReservationByTicketIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SeatReservationDto> Handle(GetSeatReservationByTicketId request, CancellationToken cancellationToken)
        {
            var seatReservation = await _context.SeatReservations.Select(sr => new
            {
                Id = sr.Id,
                TicketId = sr.Ticket.Id,
                Car = sr.Seat.Car,
                Number = sr.Seat.Number,
                TrainIdentifier = sr.Seat.Train.TrainId
            }).Where(sr => sr.TicketId == request.Id).FirstOrDefaultAsync(cancellationToken);

            var seatReservationDto = new SeatReservationDto
            {
                Id = seatReservation.Id,
                TrainIdentifier = seatReservation.TrainIdentifier,
                Car = seatReservation.Car,
                Number = seatReservation.Number
            };

            return seatReservationDto;
        }
    }
}
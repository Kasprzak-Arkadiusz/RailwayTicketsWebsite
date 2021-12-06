using Application.Common.DTOs;
using MediatR;

namespace Application.SeatReservations.Queries
{
    public class GetSeatReservationByTicketId : IRequest<SeatReservationDto>
    {
        public int Id { get; set; }
    }
}
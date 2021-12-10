using Application.Abstractions.Messaging;
using Application.Common.DTOs;

namespace Application.SeatReservations.Queries
{
    public class GetSeatReservationByTicketId : IQuery<SeatReservationDto>
    {
        public int Id { get; set; }
    }
}
using Domain.Common;
using System;

namespace Domain.Entities
{
    public class SeatReservation : BaseEntity
    {
        public Seat Seat { get; init; }

        public int? SeatReservationForeignKey { get; set; }
        public Ticket Ticket { get; set; }

        public SeatReservation(Seat seat)
        {
            Seat = seat;
            SeatReservationForeignKey = seat.Id;
        }

        public SeatReservation()
        {
            
        }
    }
}
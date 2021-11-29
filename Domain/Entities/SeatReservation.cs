using Domain.Common;
using System;

namespace Domain.Entities
{
    public class SeatReservation : BaseEntity
    {
        public DateTime TrainDepartureTime { get; set; }

        public Seat Seat { get; set; }

        public int? SeatReservationForeignKey { get; set; }
        public Ticket Ticket { get; set; }
    }
}
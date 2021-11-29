using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string OwnerId { get; set; }

        public Route Route { get; set; }
        public SeatReservation SeatReservation { get; set; }
        public Train Train { get; set; }
        public ICollection<ReturnedTicket> ReturnedTickets { get; set; }
    }
}
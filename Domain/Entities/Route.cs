using System;
using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Route : BaseEntity
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsSuspended { get; set; }
        public short NumberOfFreeSeats { get; set; }

        public Station FinalStation { get; set; }
        public Station StartingStation { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public Train Train { get; set; }
    }
}
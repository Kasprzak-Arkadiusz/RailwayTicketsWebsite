using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Route : BaseEntity
    {
        public short DepartureTimeInMinutesPastMidnight { get; set; }
        public short ArrivalTimeInMinutesPastMidnight { get; set; }
        public bool IsOnHold { get; set; }

        public Station FinalStation { get; set; }
        public Station StartingStation { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public Train Train { get; set; }
    }
}
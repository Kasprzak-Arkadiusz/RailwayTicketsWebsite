using System.Collections.Generic;

namespace Domain.Models
{
    public class Route : BaseEntity
    {
        public Route()
        {
            Tickets = new HashSet<Ticket>();
        }

        public short StartingStation { get; set; }
        public short FinalStation { get; set; }
        public short DepartureTimeInMinutesPastMidnight { get; set; }
        public short ArrivalTimeInMinutesPastMidnight { get; set; }
        public bool IsOnHold { get; set; }

        public virtual Station FinalStationNavigation { get; set; }
        public virtual Station StartingStationNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
﻿using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Route : BaseEntity
    {
        public Route()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int StartingStation { get; set; }
        public int FinalStation { get; set; }
        public short DepartureTimeInMinutesPastMidnight { get; set; }
        public short ArrivalTimeInMinutesPastMidnight { get; set; }
        public bool IsOnHold { get; set; }

        public virtual Station FinalStationNavigation { get; set; }
        public virtual Station StartingStationNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
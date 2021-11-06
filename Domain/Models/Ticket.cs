using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Ticket : BaseEntity
    {
        public Ticket()
        {
            ReturnedTickets = new HashSet<ReturnedTicket>();
        }

        public int Owner { get; set; }
        public int Route { get; set; }
        public DateTime DayOfDeparture { get; set; }
        public int Seat { get; set; }
        public short Train { get; set; }

        public virtual Route RouteNavigation { get; set; }
        public virtual Seat SeatNavigation { get; set; }
        public virtual Train TrainNavigation { get; set; }
        public virtual ICollection<ReturnedTicket> ReturnedTickets { get; set; }
    }
}
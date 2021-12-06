using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string OwnerId { get; set; }

        public Route Route { get; set; }
        public ICollection<SeatReservation> SeatReservations { get; set; }
        public Train Train { get; set; }

        public Ticket(string ownerId, Route route, Train train, SeatReservation seatReservation)
        {
            OwnerId = ownerId;
            Route = route;
            Train = train;
            SeatReservations = new List<SeatReservation> { seatReservation };
        }
        public Ticket()
        { }
    }
}
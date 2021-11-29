using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Train : BaseEntity
    {
        public short TrainId { get; set; }
        public byte NumberOfCars { get; set; }
        public short NumberOfSeats { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Route> Routes { get; set; }

        public Train()
        { }

        public Train(short trainId, byte numberOfCars, short numberOfSeats)
        {
            TrainId = trainId;
            NumberOfCars = numberOfCars;
            NumberOfSeats = numberOfSeats;
        }
    }
}
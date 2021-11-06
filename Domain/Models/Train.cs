using System.Collections.Generic;

namespace Domain.Models
{
    public class Train : BaseEntity
    {
        public Train()
        {
            Seats = new HashSet<Seat>();
            Tickets = new HashSet<Ticket>();
        }

        public short TrainId { get; set; }
        public byte NumberOfCars { get; set; }
        public short NumberOfSeats { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
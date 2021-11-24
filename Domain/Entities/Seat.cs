using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Seat : BaseEntity
    {
        public byte Car { get; set; }
        public short Number { get; set; }
        public bool? IsFree { get; set; }

        public virtual Train Train { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
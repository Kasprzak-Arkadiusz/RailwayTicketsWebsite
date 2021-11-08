﻿using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Seat : BaseEntity
    {
        public Seat()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Train { get; set; }
        public byte Car { get; set; }
        public byte Number { get; set; }
        public bool? IsFree { get; set; }

        public virtual Train TrainNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
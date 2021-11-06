using Domain.Common;
using System;

namespace Domain.Entities
{
    public class ReturnedTicket : BaseEntity
    {
        public int Ticket { get; set; }
        public DateTime DateOfReturn { get; set; }
        public string GenericReasonOfReturn { get; set; }
        public string PersonalReasonOfReturn { get; set; }

        public virtual Ticket TicketNavigation { get; set; }
    }
}
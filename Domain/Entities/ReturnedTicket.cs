using Domain.Common;
using System;

namespace Domain.Entities
{
    public class ReturnedTicket : BaseEntity
    {
        public DateTime DateOfReturn { get; set; }
        public string GenericReasonOfReturn { get; set; }
        public string PersonalReasonOfReturn { get; set; }

        public Ticket Ticket { get; set; }
    }
}
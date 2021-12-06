using System;

namespace Application.Common.DTOs
{
    public class ReturnedTicketDto
    {
        public int Id { get; set; }
        public string OwnerEmail { get; set; }
        public DateTime DateOfReturn { get; set; }
        public string GenericReasonOfReturn { get; set; }
        public string PersonalReasonOfReturn { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FinalStationName { get; set; }
        public string StartingStationName { get; set; }
        public byte Car { get; set; }
        public short Number { get; set; }
        public short TrainId { get; set; }
    }
}
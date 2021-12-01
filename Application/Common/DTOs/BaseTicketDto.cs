using System;

namespace Application.Common.DTOs
{
    public abstract class BaseTicketDto
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FinalStation { get; set; }
        public string StartingStation { get; set; }
        public short TrainIdentifier { get; set; }
        public byte Car { get; set; }
        public short SeatNumber { get; set; }
    }
}
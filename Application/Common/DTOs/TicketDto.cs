using System;

namespace Application.Common.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FinalStation { get; set; }
        public string StartingStation { get; set; }
        public short TrainId { get; set; }
        public byte Car { get; set; }
        public short SeatNumber { get; set; }
    }
}
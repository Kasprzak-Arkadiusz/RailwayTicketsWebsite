using System;

namespace WebApp.Backend.Models
{
    public class CreateTicketRequestBody
    {
        public string UserId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FinalStation { get; set; }
        public string StartingStation { get; set; }
        public short TrainId { get; set; }
        public byte Car { get; set; }
        public short SeatNumber { get; set; }
    }
}

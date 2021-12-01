using System;

namespace Application.Common.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FinalStation { get; set; }
        public string StartingStation { get; set; }
        public short TrainIdentifier { get; set; }
        public byte Car { get; set; }
        public short SeatNumber { get; set; }

        public int TrainId { get; set; }
        public int RouteId { get; set; }
        public int SeatReservationId { get; set; }
    }
}
using Domain.Entities;

namespace Application.Common.DTOs
{ 
    public class SeatDto
    {
        public byte Car { get; set; }
        public short Number { get; set; }
        public short TrainId { get; set; }
    }
}
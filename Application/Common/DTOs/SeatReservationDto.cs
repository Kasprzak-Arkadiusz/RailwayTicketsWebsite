namespace Application.Common.DTOs
{
    public class SeatReservationDto
    {
        public int Id { get; set; }
        public byte Car { get; set; }
        public short Number { get; set; }
        public short TrainIdentifier { get; set; }
    }
}
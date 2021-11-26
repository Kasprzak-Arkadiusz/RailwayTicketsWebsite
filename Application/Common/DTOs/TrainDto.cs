namespace Application.Common.DTOs
{
    public class TrainDto
    {
        public int Id { get; set; }
        public short TrainId { get; set; }
        public byte NumberOfCars { get; set; }
        public short NumberOfSeats { get; set; }
        public short NumberOfFreeSeats { get; set; }
    }
}
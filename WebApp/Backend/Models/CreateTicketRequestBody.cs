namespace WebApp.Backend.Models
{
    public class CreateTicketRequestBody
    {
        public string OwnerId { get; set; }
        public short TrainId { get; set; }
        public int RouteId { get; set; }
        public int SeatReservationId { get; set; }
    }
}
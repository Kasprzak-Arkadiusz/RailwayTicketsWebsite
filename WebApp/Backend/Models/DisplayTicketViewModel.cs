using Application.Common.DTOs;

namespace WebApp.Backend.Models
{
    public class DisplayTicketViewModel : BaseTicketDto
    {
        public int TrainId { get; set; }
        public int RouteId { get; set; }
        public int SeatReservationId { get; set; }
    }
}
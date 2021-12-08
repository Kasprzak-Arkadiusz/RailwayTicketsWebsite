using System;
using System.ComponentModel;

namespace WebApp.Frontend.ViewModels
{
    public class DisplayTicketViewModel
    {
        [DisplayName("Departure time:")]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Arrival time:")]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("From:")]
        public string StartingStation { get; set; }

        [DisplayName("To:")]
        public string FinalStation { get; set; }

        [DisplayName("Train identifier:")]
        public short TrainIdentifier { get; set; }

        [DisplayName("Car number:")]
        public byte Car { get; set; }

        [DisplayName("Seat number:")]
        public short SeatNumber { get; set; }

        public int TrainId { get; set; }
        public int RouteId { get; set; }
        public int SeatReservationId { get; set; }
    }
}
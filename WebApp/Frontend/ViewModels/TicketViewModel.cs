using System;
using System.ComponentModel;

namespace WebApp.Frontend.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }

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
    }
}

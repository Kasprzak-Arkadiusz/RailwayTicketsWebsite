using System;
using System.ComponentModel;

namespace WebApp.Frontend.ViewModels
{
    public class ReturnedTicketViewModel
    {
        public int Id { get; set; }
        [DisplayName("Email of the person returning the ticket:")]
        public string OwnerEmail { get; set; }
        [DisplayName("Date of return:")]
        public DateTime DateOfReturn { get; set; }

        [DisplayName("Generic reason of return:")]
        public string GenericReasonOfReturn { get; set; }

        [DisplayName("Personal reason of return:")]
        public string PersonalReasonOfReturn { get; set; }

        [DisplayName("Departure time:")]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Arrival time:")]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Final station:")]
        public string FinalStationName { get; set; }

        [DisplayName("Starting station:")]
        public string StartingStationName { get; set; }

        [DisplayName("Car number:")]
        public byte Car { get; set; }

        [DisplayName("Seat number:")]
        public short Number { get; set; }

        [DisplayName("Train identifier:")]
        public short TrainIdentifier { get; set; }
    }
}
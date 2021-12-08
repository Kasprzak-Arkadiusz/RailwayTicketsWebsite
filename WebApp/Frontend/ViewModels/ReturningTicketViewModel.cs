using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Frontend.ViewModels
{
    public class ReturningTicketViewModel
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

        [DisplayName("Car number:")]
        public byte Car { get; set; }

        [DisplayName("Seat number:")]
        public short SeatNumber { get; set; }

        public IList<SelectListItem> GenericReasonsOfReturn { get; set; }

        [Required]
        [DisplayName("Generic reason of return:")]
        public string GenericReasonOfReturn { get; set; }

        [Required]
        [DisplayName("Personal reason of return:")]
        public string PersonalReasonOfReturn { get; set; }
    }
}
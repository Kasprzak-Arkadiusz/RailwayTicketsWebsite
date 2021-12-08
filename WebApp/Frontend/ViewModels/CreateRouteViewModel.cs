using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Frontend.ViewModels
{
    public class CreateRouteViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Departure time:")]
        public DateTime DepartureTime { get; set; }

        [Required]
        [DisplayName("Arrival time:")]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Is suspended?")]
        public bool IsSuspended { get; set; }

        [Required]
        [DisplayName("From:")]
        public string StartingStation { get; set; }

        public IList<SelectListItem> StartingStations { get; set; }

        [Required]
        [DisplayName("To:")]
        public string FinalStation { get; set; }

        public IList<SelectListItem> FinalStations { get; set; }

        [Required]
        [DisplayName("Train identifier:")]
        public short TrainId { get; set; }

        public IList<SelectListItem> TrainIds { get; set; }
    }
}
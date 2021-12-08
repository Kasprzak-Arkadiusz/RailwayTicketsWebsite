using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Frontend.ViewModels
{
    public class FindRoutesViewModel
    {
        public string From { get; init; }

        public string To { get; init; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Departure time")]
        public DateTime? DepartureTime { get; init; }

        public bool Suspended { get; set; }
    }
}
using Domain.Common;
using System;

namespace Domain.Entities
{
    public class ReturnedTicket : BaseEntity
    {
        public string OwnerEmail { get; set; }
        public DateTime DateOfReturn { get; set; }
        public string GenericReasonOfReturn { get; set; }
        public string PersonalReasonOfReturn { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FinalStationName { get; set; }
        public string StartingStationName { get; set; }
        public byte Car { get; set; }
        public short Number { get; set; }
        public short TrainId { get; set; }

        public ReturnedTicket(string email, string genericReason, string personalReason, Ticket ticket, Seat seat, DateTime? dateOfReturn = null)
        {
            OwnerEmail = email;
            DateOfReturn = dateOfReturn ?? DateTime.Now;;
            GenericReasonOfReturn = genericReason;
            PersonalReasonOfReturn = personalReason;
            DepartureTime = ticket.Route.DepartureTime;
            ArrivalTime = ticket.Route.ArrivalTime;
            FinalStationName = ticket.Route.FinalStation.Name;
            StartingStationName = ticket.Route.StartingStation.Name;
            Car = seat.Car;
            Number = seat.Number;
            TrainId = ticket.Route.Train.TrainId;
        }

        public ReturnedTicket()
        {
        }
    }
}
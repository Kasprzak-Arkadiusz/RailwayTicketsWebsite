using MediatR;
using System;

namespace Application.Routes.Commands.UpdateRoute
{
    public class UpdateRouteCommand : IRequest
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsOnHold { get; set; }
        public string StartingStation { get; set; }
        public string FinalStation { get; set; }
        public short TrainId { get; set; }
    }
}
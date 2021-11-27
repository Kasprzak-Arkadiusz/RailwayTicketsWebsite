using MediatR;
using System;

namespace Application.Routes.Commands.CreateRoute
{
    public class CreateRouteCommand : IRequest<int>
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsOnHold { get; set; }
        public string StartingStation { get; set; }
        public string FinalStation { get; set; }
        public short TrainId { get; set; }
    }
}
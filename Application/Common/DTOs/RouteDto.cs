﻿using System;

namespace Application.Common.DTOs
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string StartingStation { get; set; }
        public string FinalStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsOnHold { get; set; }
        public short TrainId { get; set; }
    }
}
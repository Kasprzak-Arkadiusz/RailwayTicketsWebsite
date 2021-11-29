using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Seat : BaseEntity
    {
        //TODO check if Car can have private setter
        public byte Car { get; set; }
        public short Number { get; set; }
        public Train Train { get; set; }

        public int? SeatForeignKey { get; set; }
        public SeatReservation SeatReservation { get; set; }

        public Seat()
        { }

        public Seat(short seatNumber, Train train)
        {
            Number = seatNumber;
            Train = train;
            Car = CalculateCarNumber(train, seatNumber);
        }

        private static byte CalculateCarNumber(Train train, int seatNumber)
        {
            var numberOfSeatsInCar = train.NumberOfSeats / train.NumberOfCars;
            return (byte)(seatNumber / numberOfSeatsInCar + 1);
        }
    }
}
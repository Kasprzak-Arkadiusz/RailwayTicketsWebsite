using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Seat : BaseEntity
    {
        public byte Car { get; set; }
        public short Number { get; set; }
        public bool? IsFree { get; set; }

        public Train Train { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public Seat()
        { }

        public Seat(short seatNumber, Train train)
        {
            Number = seatNumber;
            IsFree = true;
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
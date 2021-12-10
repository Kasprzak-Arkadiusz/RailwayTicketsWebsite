using Application.Abstractions.Messaging;
using Application.Common.DTOs;

namespace Application.Seats.Commands.CreateSeat
{
    public class CreateSeatCommand : ICommand<SeatDto>
    {
        public int TrainId { get; set; }
    }
}
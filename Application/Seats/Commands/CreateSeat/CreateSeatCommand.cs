using Application.Common.DTOs;
using MediatR;

namespace Application.Seats.Commands.CreateSeat
{
    public class CreateSeatCommand : IRequest<SeatDto>
    {
        public int TrainId { get; set; }
    }
}
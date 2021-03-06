using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seats.Commands.CreateSeat
{
    public class CreateSeatCommandHandler : ICommandHandler<CreateSeatCommand, SeatDto>
    {
        private readonly IApplicationDbContext _context;

        public CreateSeatCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SeatDto> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
        {
            var train = await _context.Trains.SingleOrDefaultAsync(
                t => t.TrainId == request.TrainId, cancellationToken);
            if (train == null)
            {
                throw new NotFoundException("A train couldn't be found.");
            }

            var createdSeats = await _context.Seats
                .Include(s => s.Train)
                .Include(s => s.SeatReservation)
                .Where(s => s.Train.TrainId == train.TrainId).ToListAsync(cancellationToken);

            var freeSeatsInTrain = createdSeats.Where(s => s.SeatReservation == null).ToList();

            if (freeSeatsInTrain.Count != 0)
                return new SeatDto
                {
                    Id = freeSeatsInTrain[0].Id,
                    TrainId = train.TrainId,
                    Car = freeSeatsInTrain[0].Car,
                    Number = freeSeatsInTrain[0].Number
                };

            if (createdSeats.Count - freeSeatsInTrain.Count == train.NumberOfSeats)
            {
                throw new InvalidOperationException("The train is full.");
            }

            var seatNumber = Enumerable.Range(1, train.NumberOfSeats).First(n => createdSeats.All(s => s.Number != n));

            var seat = new Seat((short)(seatNumber), train);
            await _context.Seats.AddAsync(seat, cancellationToken);
            await _context.SaveChangesAsync();

            return new SeatDto
            {
                Id = seat.Id,
                TrainId = train.TrainId,
                Car = seat.Car,
                Number = seat.Number
            };
        }
    }
}
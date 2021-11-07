using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Stations.Any())
            {
                context.Stations.AddRange(
                    new Station { Name = "Warszawa Centralna", },
                    new Station { Name = "Gdańsk Główny" },
                    new Station { Name = "Poznań Wschód" },
                    new Station { Name = "Kraków Główny" },
                    new Station { Name = "Warszawa Zachodnia" },
                    new Station { Name = "Dąbrowa Górnicza Huta Katowice" });

                context.Trains.AddRange(
                    new Train
                    {
                        TrainId = 1024,
                        NumberOfCars = 8,
                        NumberOfSeats = 256
                    },
                    new Train
                    {
                        TrainId = 1769,
                        NumberOfCars = 6,
                        NumberOfSeats = 288
                    },
                    new Train
                    {
                        TrainId = 317,
                        NumberOfCars = 5,
                        NumberOfSeats = 240
                    }
                );
                await context.SaveChangesAsync();

                var trains = await context.Trains.ToListAsync();
                var seats = new List<Seat>();

                foreach (var train in trains)
                {
                    int numberOfSeatsInCar = train.NumberOfSeats / train.NumberOfCars;
                    for (int i = 0; i < train.NumberOfSeats; i++)
                    {
                        seats.Add(new Seat
                        {
                            Train = train.Id,
                            Car = (byte)((i / numberOfSeatsInCar ) + 1),
                            Number = (byte)(i + 1),
                            IsFree = true
                        });
                    }
                }
                context.Seats.AddRange(seats);

                var stations = await context.Stations.ToListAsync();
                context.Routes.AddRange(
                    new Route
                    {
                        StartingStation = stations[0].Id,
                        FinalStation = stations[1].Id,
                        DepartureTimeInMinutesPastMidnight = 480,
                        ArrivalTimeInMinutesPastMidnight = 780,
                        IsOnHold = false
                    },
                    new Route
                    {
                        StartingStation = stations[2].Id,
                        FinalStation = stations[3].Id,
                        DepartureTimeInMinutesPastMidnight = 720,
                        ArrivalTimeInMinutesPastMidnight = 1020,
                        IsOnHold = false
                    },
                    new Route
                    {
                        StartingStation = stations[4].Id,
                        FinalStation = stations[5].Id,
                        DepartureTimeInMinutesPastMidnight = 1200,
                        ArrivalTimeInMinutesPastMidnight = 60,
                        IsOnHold = true
                    });
                await context.SaveChangesAsync();

                var routes = await context.Routes.ToListAsync();
                //Change Owner Id when Identity users will be implemented
                context.Tickets.AddRange(
                    new Ticket
                    {
                        Owner = 1,
                        DayOfDeparture = DateTime.Now,
                        Route = routes[0].Id,
                        Train = trains[0].Id,
                        Seat = 64
                    },
                    new Ticket
                    {
                        Owner = 2,
                        DayOfDeparture = new DateTime(2021, 12, 20),
                        Route = routes[1].Id,
                        Train = trains[1].Id,
                        Seat = 1
                    },
                    new Ticket
                    {
                        Owner = 3,
                        DayOfDeparture = new DateTime(2021, 12, 6),
                        Route = routes[2].Id,
                        Train = trains[2].Id,
                        Seat = 240
                    });
                await context.SaveChangesAsync();

                //Change GenericReasonOfReturn when enum for it will be created
                var tickets = await context.Tickets.ToListAsync();
                context.ReturnedTickets.AddRange(
                    new ReturnedTicket
                    {
                        Ticket = tickets[1].Id,
                        DateOfReturn = new DateTime(2021, 11, 6),
                        GenericReasonOfReturn = "The reason for my ride is no longer valid.",
                        PersonalReasonOfReturn = "Due to change in my personal affairs i don't see point in traveling by train."
                    });
                await context.SaveChangesAsync();
            }
        }
    }
}
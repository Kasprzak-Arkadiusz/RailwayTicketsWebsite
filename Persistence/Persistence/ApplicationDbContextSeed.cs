using Application.Common.Constants;
using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(IApplicationDbContext context, IIdentityService identityService)
        {
            if (!context.Stations.Any())
            {
                var stations = new List<Station>
                {
                    new() { Name = "Warszawa Centralna", },
                    new() { Name = "Gdańsk Główny" },
                    new() { Name = "Poznań Wschód" },
                    new() { Name = "Kraków Główny" },
                    new() { Name = "Warszawa Zachodnia" },
                    new() { Name = "Dąbrowa Górnicza Huta Katowice" }
                };
                context.Stations.AddRange(stations);

                var trains = new List<Train>
                {
                    new (1024, 8, 256),
                    new (1769, 6, 288),
                    new (317, 5, 240),
                    new (1413, 7, 224),
                    new (123, 8, 256)
                };
                context.Trains.AddRange(trains);

                var routes = new List<Route>
                {
                    new()
                    {
                        StartingStation = stations[0],
                        FinalStation = stations[1],
                        DepartureTime = new DateTime(2021, 12, 18, 8, 0, 0),
                        ArrivalTime = new DateTime(2021, 12, 18, 13, 0, 0),
                        NumberOfFreeSeats = --trains[0].NumberOfSeats,
                        IsSuspended = false,
                        Train = trains[0]
                    },
                    new()
                    {
                        StartingStation = stations[2],
                        FinalStation = stations[3],
                        DepartureTime = new DateTime(2021, 12, 18, 12, 0, 0),
                        ArrivalTime = new DateTime(2021, 12, 18, 17, 0, 0),
                        NumberOfFreeSeats = --trains[1].NumberOfSeats,
                        IsSuspended = false,
                        Train = trains[1]
                    },
                    new()
                    {
                        StartingStation = stations[4],
                        FinalStation = stations[5],
                        DepartureTime = new DateTime(2021, 12, 18, 20, 0, 0),
                        ArrivalTime = new DateTime(2021, 12, 19, 1, 0, 0),
                        NumberOfFreeSeats = --trains[2].NumberOfSeats,
                        IsSuspended = true,
                        Train = trains[2]
                    },
                    new()
                    {
                        StartingStation = stations[5],
                        FinalStation = stations[1],
                        DepartureTime = new DateTime(2021, 12, 19, 4, 0, 0),
                        ArrivalTime = new DateTime(2021, 12, 20, 9, 0, 0),
                        NumberOfFreeSeats = --trains[3].NumberOfSeats,
                        IsSuspended = false,
                        Train = trains[3]
                    }
                };
                context.Routes.AddRange(routes);

                var seats = new List<Seat>
                {
                    new(64, trains[0]),
                    new(1, trains[1]),
                    new(240, trains[2]),
                    new(111, trains[3])
                };
                context.Seats.AddRange(seats);

                var seatReservations = new List<SeatReservation>()
                {
                    new(seats[0]),
                    new(seats[1]),
                    new(seats[2]),
                    new(seats[3])
                };
                context.SeatReservations.AddRange(seatReservations);

                var userId = await identityService.GetUserIdByUserName("justAnUser");

                var tickets = new List<Ticket>
                {
                    new(userId, routes[0], trains[0], seatReservations[0]),
                    new(userId, routes[1], trains[1], seatReservations[1]),
                    new(userId, routes[2], trains[2], seatReservations[2]),
                    new(userId, routes[3], trains[3], seatReservations[3])
                };
                context.Tickets.AddRange(tickets);

                var userEmail = await identityService.GetUserEmailAsync(userId);

                var returnedTickets = new List<ReturnedTicket>
                {
                    new(userEmail, GenericReasonsOfReturn.ReasonsList[1],
                        "Due to change in my personal affairs i don't see point in traveling by train", tickets[1], seats[1],new DateTime(2021, 11, 6))
                };

                context.ReturnedTickets.AddRange(returnedTickets);
                await context.SaveChangesAsync();
            }
        }
    }
}
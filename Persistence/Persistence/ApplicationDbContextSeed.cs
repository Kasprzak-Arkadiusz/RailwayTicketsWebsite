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
                    new (317, 5, 240)
                };
                context.Trains.AddRange(trains);

                /*await context.SaveChangesAsync();*/

                var routes = new List<Route>
                {
                    new()
                    {
                        StartingStation = stations[0],
                        FinalStation = stations[1],
                        DepartureTimeInMinutesPastMidnight = 480,
                        ArrivalTimeInMinutesPastMidnight = 780,
                        IsOnHold = false,
                        Train = trains[0]
                    },
                    new()
                    {
                        StartingStation = stations[2],
                        FinalStation = stations[3],
                        DepartureTimeInMinutesPastMidnight = 720,
                        ArrivalTimeInMinutesPastMidnight = 1020,
                        IsOnHold = false,
                        Train = trains[1]
                    },
                    new()
                    {
                        StartingStation = stations[4],
                        FinalStation = stations[5],
                        DepartureTimeInMinutesPastMidnight = 1200,
                        ArrivalTimeInMinutesPastMidnight = 60,
                        IsOnHold = true,
                        Train = trains[2]
                    }
                };
                context.Routes.AddRange(routes);

                var seats = new List<Seat>
                {
                    new(64, trains[0]),
                    new(1, trains[1]),
                    new(240, trains[2])
                };
                context.Seats.AddRange(seats);

                /*await context.SaveChangesAsync();
                */

                var userId = await identityService.GetUserIdByUserName("justAnUser");

                var tickets = new List<Ticket>
                {
                    new()
                    {
                        OwnerId = userId,
                        DayOfDeparture = DateTime.Now,
                        Route = routes[0],
                        Train = trains[0],
                        Seat = seats[0]
                    },
                    new()
                    {
                        OwnerId = userId,
                        DayOfDeparture = new DateTime(2021, 12, 20),
                        Route = routes[1],
                        Train = trains[1],
                        Seat = seats[1]
                    },
                    new()
                    {
                        OwnerId = userId,
                        DayOfDeparture = new DateTime(2021, 12, 6),
                        Route = routes[2],
                        Train = trains[2],
                        Seat = seats[2]
                    }
                };
                context.Tickets.AddRange(tickets);

                /*
                await context.SaveChangesAsync();*/

                //Change GenericReasonOfReturn when enum for it will be created
                var returnedTickets = new List<ReturnedTicket>
                {
                    new()
                    {
                        Ticket = tickets[1],
                        DateOfReturn = new DateTime(2021, 11, 6),
                        GenericReasonOfReturn = "The reason for my ride is no longer valid.",
                        PersonalReasonOfReturn = "Due to change in my personal affairs i don't see point in traveling by train."
                    }
                };

                context.ReturnedTickets.AddRange(returnedTickets);
                await context.SaveChangesAsync();
            }
        }
    }
}
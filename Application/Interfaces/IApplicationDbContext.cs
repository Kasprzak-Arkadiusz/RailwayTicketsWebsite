using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<ReturnedTicket> ReturnedTickets { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }

        Task<int> SaveChanges();
    }
}
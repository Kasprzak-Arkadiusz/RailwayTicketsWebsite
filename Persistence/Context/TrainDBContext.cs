using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class TrainDbContext : DbContext, IApplicationDbContext
    {
        public TrainDbContext()
        {
        }

        public TrainDbContext(DbContextOptions<TrainDbContext> options)
            : base(options) { }

        public DbSet<ReturnedTicket> ReturnedTickets { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS1;Database=TrainDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<ReturnedTicket>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfReturn)
                    .HasColumnType("date")
                    .HasColumnName("dateOfReturn");

                entity.Property(e => e.GenericReasonOfReturn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("genericReasonOfReturn");

                entity.Property(e => e.PersonalReasonOfReturn)
                    .HasMaxLength(200)
                    .HasColumnName("personalReasonOfReturn");

                entity.Property(e => e.Ticket).HasColumnName("ticket");

                entity.HasOne(d => d.TicketNavigation)
                    .WithMany(p => p.ReturnedTickets)
                    .HasForeignKey(d => d.Ticket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ReturnedTickets_Tickets");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArrivalTimeInMinutesPastMidnight).HasColumnName("arrivalTimeInMinutesPastMidnight");

                entity.Property(e => e.DepartureTimeInMinutesPastMidnight).HasColumnName("departureTimeInMinutesPastMidnight");

                entity.Property(e => e.FinalStation).HasColumnName("finalStation");

                entity.Property(e => e.IsOnHold).HasColumnName("isOnHold");

                entity.Property(e => e.StartingStation).HasColumnName("startingStation");

                entity.HasOne(d => d.FinalStationNavigation)
                    .WithMany(p => p.RouteFinalStationNavigations)
                    .HasForeignKey(d => d.FinalStation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_finalStation_Stations");

                entity.HasOne(d => d.StartingStationNavigation)
                    .WithMany(p => p.RouteStartingStationNavigations)
                    .HasForeignKey(d => d.StartingStation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_startingStation_Stations");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Car).HasColumnName("car");

                entity.Property(e => e.IsFree)
                    .IsRequired()
                    .HasColumnName("isFree")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Train).HasColumnName("train");

                entity.HasOne(d => d.TrainNavigation)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.Train)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Seats_Trains");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DayOfDeparture)
                    .HasColumnType("date")
                    .HasColumnName("dayOfDeparture");

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.Route).HasColumnName("route");

                entity.Property(e => e.Seat).HasColumnName("seat");

                entity.Property(e => e.Train).HasColumnName("train");

                entity.HasOne(d => d.RouteNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.Route)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tickets_Routes");

                entity.HasOne(d => d.SeatNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.Seat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tickets_Seats");

                entity.HasOne(d => d.TrainNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.Train)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tickets_Trains");
            });

            modelBuilder.Entity<Train>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NumberOfCars).HasColumnName("numberOfCars");

                entity.Property(e => e.NumberOfSeats).HasColumnName("numberOfSeats");

                entity.Property(e => e.TrainId).HasColumnName("trainId");
            });

            base.OnModelCreating(modelBuilder);
        }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
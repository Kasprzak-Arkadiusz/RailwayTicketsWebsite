using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.DayOfDeparture)
                .HasColumnType("date")
                .HasColumnName("dayOfDeparture");

            builder.Property(t => t.Owner).HasColumnName("owner");

            builder.Property(t => t.Route).HasColumnName("route");

            builder.Property(t => t.Seat).HasColumnName("seat");

            builder.Property(t => t.Train).HasColumnName("train");

            builder.HasOne(ti => ti.RouteNavigation)
                .WithMany(r => r.Tickets)
                .HasForeignKey(ti => ti.Route)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Tickets_Routes");

            builder.HasOne(ti => ti.SeatNavigation)
                .WithMany(s => s.Tickets)
                .HasForeignKey(ti => ti.Seat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Tickets_Seats");

            builder.HasOne(ti => ti.TrainNavigation)
                .WithMany(tr => tr.Tickets)
                .HasForeignKey(ti => ti.Train)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Tickets_Trains");
        }
    }
}
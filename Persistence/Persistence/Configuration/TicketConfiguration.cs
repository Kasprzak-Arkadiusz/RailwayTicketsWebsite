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

            builder.Property(t => t.OwnerId).HasColumnName("ownerId");

            builder.HasOne(ti => ti.Route)
                .WithMany(r => r.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Tickets_Routes");

            builder.HasOne(ti => ti.Seat)
                .WithMany(s => s.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Tickets_Seats");

            builder.HasOne(ti => ti.Train)
                .WithMany(tr => tr.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Tickets_Trains");

            builder.HasMany(d => d.ReturnedTickets)
                .WithOne(p => p.Ticket)
                .HasConstraintName("fk_ReturnedTickets_Tickets");
        }
    }
}
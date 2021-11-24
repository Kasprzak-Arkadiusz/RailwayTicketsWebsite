using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.Property(r => r.Id).HasColumnName("id");

            builder.Property(r => r.ArrivalTimeInMinutesPastMidnight).HasColumnName("arrivalTimeInMinutesPastMidnight");

            builder.Property(r => r.DepartureTimeInMinutesPastMidnight).HasColumnName("departureTimeInMinutesPastMidnight");

            builder.Property(r => r.IsOnHold).HasColumnName("isOnHold");

            builder.HasOne(d => d.FinalStation)
                .WithMany(p => p.RouteFinalStation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_finalStation_Stations");

            builder.HasOne(d => d.StartingStation)
                .WithMany(p => p.RouteStartingStation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_startingStation_Stations");
        }
    }
}
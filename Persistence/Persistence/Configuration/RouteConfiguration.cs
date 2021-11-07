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

            builder.Property(r => r.FinalStation).HasColumnName("finalStation");

            builder.Property(r => r.IsOnHold).HasColumnName("isOnHold");

            builder.Property(r => r.StartingStation).HasColumnName("startingStation");

            builder.HasOne(d => d.FinalStationNavigation)
                .WithMany(p => p.RouteFinalStationNavigations)
                .HasForeignKey(d => d.FinalStation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_finalStation_Stations");

            builder.HasOne(d => d.StartingStationNavigation)
                .WithMany(p => p.RouteStartingStationNavigations)
                .HasForeignKey(d => d.StartingStation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_startingStation_Stations");
        }
    }
}
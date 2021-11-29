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

            builder.Property(r => r.ArrivalTime).HasColumnName("arrivalTime");

            builder.Property(r => r.DepartureTime).HasColumnName("departureTime");

            builder.Property(r => r.IsSuspended).HasColumnName("isSuspended");

            builder.HasOne(d => d.FinalStation)
                .WithMany(p => p.RouteFinalStation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_finalStation_Stations");

            builder.HasOne(d => d.StartingStation)
                .WithMany(p => p.RouteStartingStation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_startingStation_Stations");

            builder.HasOne(d => d.Train)
                .WithMany(p => p.Routes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_train_Routes");
        }
    }
}
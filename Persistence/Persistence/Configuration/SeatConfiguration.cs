using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.Car).HasColumnName("car");

            builder.Property(s => s.IsFree)
                .IsRequired()
                .HasColumnName("isFree")
                .HasDefaultValueSql("((1))");

            builder.Property(s => s.Number).HasColumnName("number");

            builder.HasOne(d => d.Train)
                .WithMany(p => p.Seats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Seats_Trains");
        }
    }
}
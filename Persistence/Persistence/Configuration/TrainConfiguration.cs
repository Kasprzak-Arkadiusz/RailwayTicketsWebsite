using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TrainConfiguration : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.NumberOfCars).HasColumnName("numberOfCars");

            builder.Property(t => t.NumberOfSeats).HasColumnName("numberOfSeats");

            builder.Property(t => t.TrainId).HasColumnName("trainId");
        }
    }
}
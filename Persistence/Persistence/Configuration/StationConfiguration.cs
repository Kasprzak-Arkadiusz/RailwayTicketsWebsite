using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class StationConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
        }
    }
}
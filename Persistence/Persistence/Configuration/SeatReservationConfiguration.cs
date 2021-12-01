using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class SeatReservationConfiguration : IEntityTypeConfiguration<SeatReservation>
    {
        public void Configure(EntityTypeBuilder<SeatReservation> builder)
        {
            
        }
    }
}
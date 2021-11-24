using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ReturnedTicketConfiguration : IEntityTypeConfiguration<ReturnedTicket>
    {
        public void Configure(EntityTypeBuilder<ReturnedTicket> builder)
        {
            builder.Property(rt => rt.Id).HasColumnName("id");

            builder.Property(rt => rt.DateOfReturn)
                .HasColumnType("date")
                .HasColumnName("dateOfReturn");

            builder.Property(rt => rt.GenericReasonOfReturn)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("genericReasonOfReturn");

            builder.Property(rt => rt.PersonalReasonOfReturn)
                .HasMaxLength(200)
                .HasColumnName("personalReasonOfReturn");
        }
    }
}
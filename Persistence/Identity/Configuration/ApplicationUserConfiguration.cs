using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Configuration
{
    internal class ApplicationUserConfiguration : IdentityUser, IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(a => a.UserName)
                .HasMaxLength(40)
                .IsRequired();
            builder.Property(a => a.NormalizedUserName)
                .HasMaxLength(40)
                .IsRequired();
            builder.Property(a => a.FirstName)
                .HasMaxLength(40)
                .IsRequired();
            builder.Property(a => a.LastName)
                .HasMaxLength(40)
                .IsRequired();
            builder.Property(a => a.PhoneNumber)
                .HasMaxLength(20);
            builder.Property(a => a.PasswordHash)
                .HasMaxLength(256);
            builder.Property(a => a.SecurityStamp)
                .HasMaxLength(256);
            builder.Property(a => a.ConcurrencyStamp)
                .HasMaxLength(256);
        }
    }
}
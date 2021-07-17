using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApplication.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {

            builder.Property(t => t.CreatedByIp)
                .HasMaxLength(400);
            builder.Property(t => t.RevokedByIp)
           .HasMaxLength(400);
        }
    }
}
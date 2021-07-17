using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApplication.Infrastructure.Persistence.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            //builder.Ignore(e => e.DomainEvents);
            builder.Property(t => t.Id)
        .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

          
         //   builder.Property(t => t.Image)
         //.HasMaxLength(2048);
        }
    }
}
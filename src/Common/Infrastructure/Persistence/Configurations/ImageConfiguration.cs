using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApplication.Infrastructure.Persistence.Configurations
{
    class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            //builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Name)
                 .HasMaxLength(300)
                 .IsRequired();

            builder.Property(t => t.extension)
                 .HasMaxLength(40);

            //builder.Property(t => t.ProfileImage)
            //   .HasMaxLength(2048);
            builder.HasKey(t=>t.Id);
            builder.Property(t => t.Id)
             .ValueGeneratedOnAdd();


        }
    }
}

using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApplication.Infrastructure.Persistence.Configurations
{
    class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.KeyName)
                 .HasMaxLength(1024)
                 .IsRequired();
            

            //builder.Property(t => t.LastName)
            //     .HasMaxLength(300)
            //     .IsRequired();

            //builder.Property(t => t.ProfileTag)
            //   .HasMaxLength(2048);
            builder.HasKey(t=>t.Id);
            builder.Property(t => t.Id)
             .ValueGeneratedOnAdd();


        }
    }
}

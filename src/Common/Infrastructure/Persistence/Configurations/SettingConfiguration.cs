using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Infrastructure.Persistence.Configurations
{
    class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            //builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Name)
                 .HasMaxLength(200)
                 .IsRequired();
            builder.Property(t => t.InputType)
               .HasMaxLength(300);

            builder.Property(t => t.Id)
             .ValueGeneratedOnAdd();


        }
    }
}

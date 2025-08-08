using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senswise.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Senswise.Persistence.Configs
{
    internal class CustomerConfig : AuditableEntityConfig<Customer>
    {
        protected override void ConfigureAuditableEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer")
                   .HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                   .HasColumnName("FirstName")
                   .HasMaxLength(75)
                   .IsRequired(true);

            builder.Property(x => x.LastName)
                   .HasColumnName("LastName")
                   .HasMaxLength(75)
                   .IsRequired(true);

            builder.Property(x => x.Email)
                   .HasColumnName("Email")
                   .HasMaxLength(255)
                   .IsRequired(true);

            builder.Property(x => x.Address)
               .HasColumnName("Address")
               .HasMaxLength(255)
               .IsRequired(false);

        }
    }
}

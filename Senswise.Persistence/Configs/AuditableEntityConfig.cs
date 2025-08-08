using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Senswise.Persistence.Configs
{
    internal abstract class AuditableEntityConfig<TEntity> : Entityconfig<TEntity> where TEntity : AuditableEntity
    {
        protected override void ConfigureEntity(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedBy)
                   .HasColumnName("CreatedBy")
                   .HasMaxLength(75)
                   .IsRequired();

            builder.Property(x => x.CreatedDate)
                   .HasColumnName("CreatedDate")
                   .IsRequired();

            builder.Property(x => x.UpdatedBy)
                   .HasColumnName("UpdatedBy")
                   .HasMaxLength(25)
                   .IsRequired(false);

            builder.Property(x => x.UpdatedDate)
                   .HasColumnName("UpdatedDate")
                   .IsRequired(false);

            ConfigureAuditableEntity(builder);
        }
        protected abstract void ConfigureAuditableEntity(EntityTypeBuilder<TEntity> builder);
    }
}
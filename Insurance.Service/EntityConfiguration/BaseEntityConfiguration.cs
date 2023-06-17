using Insurance.Common.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insurance.Service.EntityConfiguration;

public static class BaseEntityConfiguration
{
    public static void ApplyBaseEntityConfiguration<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Version).IsRequired().IsRowVersion().IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
        builder.Property(x => x.CreateUser).IsRequired().IsUnicode(false).HasMaxLength(100);
    }
}
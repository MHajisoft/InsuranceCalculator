using Insurance.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insurance.Service.EntityConfiguration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ApplyBaseEntityConfiguration();

        builder.Property(x => x.FirstName).IsUnicode().IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().IsUnicode().HasMaxLength(70);
        builder.Property(x => x.NationalCode).HasMaxLength(10).IsFixedLength().IsUnicode(false);
        builder.HasIndex(x => x.NationalCode).IsUnique();
    }
}
using Insurance.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insurance.Service.EntityConfiguration;

public class InsuranceTypeConfiguration : IEntityTypeConfiguration<InsuranceType>
{
    public void Configure(EntityTypeBuilder<InsuranceType> builder)
    {
        builder.ApplyBaseEntityConfiguration();

        builder.Property(x => x.Title).IsUnicode().IsRequired().HasMaxLength(150);
        builder.Property(x => x.MinInvest).IsRequired();
        builder.Property(x => x.MaxInvest).IsRequired();
        builder.Property(x => x.PaymentFactor).IsRequired();
    }
}
using Insurance.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insurance.Service.EntityConfiguration;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ApplyBaseEntityConfiguration();

        builder.HasOne(x => x.Person).WithMany().HasForeignKey(x => x.PersonId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Investment).IsRequired();
        builder.Property(x => x.Payment).IsRequired();
    }
}
using System.Reflection;
using Insurance.Common.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Service.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, long>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>().Property(x => x.Title).IsRequired().IsUnicode().HasMaxLength(100);
        builder.Entity<AppRole>().Property(x => x.Title).IsRequired().IsUnicode().HasMaxLength(100);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<InsuranceType> InsuranceTypes { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<Request> Requests { get; set; }
}
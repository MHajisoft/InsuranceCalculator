using System.Reflection;
using Insurance.Common.Entity;
using Insurance.Service.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Service.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, long>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>().Property(x => x.Title).IsRequired().IsUnicode().HasMaxLength(100);
        builder.Entity<AppRole>().Property(x => x.Title).IsRequired().IsUnicode().HasMaxLength(100);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var username = _httpContextAccessor.HttpContext?.User.GetUsername();

        #region Create Data

        var addedEntities = ChangeTracker.Entries().Where(entry => entry is { Entity: BaseEntity, State: EntityState.Added }).ToList();

        addedEntities.ForEach(entry =>
        {
            entry.Property(nameof(BaseEntity.CreateDate)).CurrentValue = DateTime.Now;
            entry.Property(nameof(BaseEntity.CreateUser)).CurrentValue = username ?? "auto";
        });

        #endregion

        #region Update Data

        //TODo Implement Update Info

        #endregion

        #region Delete Data
        
        //TODo Implement Update Info
        
        #endregion

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    #region Tables

    public DbSet<InsuranceType> InsuranceTypes { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<Request> Requests { get; set; }

    #endregion
}
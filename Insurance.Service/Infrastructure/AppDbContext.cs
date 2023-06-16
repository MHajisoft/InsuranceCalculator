using Insurance.Common.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Insurance.Service.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, long>
{
}
using Insurance.Common.Entity;
using Insurance.Service.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Configuration;

public class MigrationUtil
{
    public static async void Execute(WebApplication app)
    {
        const string password = "1qaz!QAZ";

        try
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            await dbContext.Database.MigrateAsync();

            if (!userManager.Users.Any())
            {
                var adminRole = new AppRole { Name = "Admin", Title = "مدیر سیستم" };
                await roleManager.CreateAsync(adminRole);

                var adminUser = new AppUser { UserName = "admin", SecurityStamp = Guid.NewGuid().ToString(), Title = "ادمین" };
                var user1 = new AppUser { UserName = "user1", SecurityStamp = Guid.NewGuid().ToString(), Title = "کاربر" };

                await userManager.CreateAsync(adminUser, password);
                await userManager.CreateAsync(user1, password);

                adminUser = await userManager.FindByNameAsync("admin");

                await userManager.AddToRoleAsync(adminUser, "Admin");

                await dbContext.InsuranceTypes.AddRangeAsync(new List<InsuranceType>
                {
                    new()
                    {
                        Title = "پوشش جراحی", MinInvest = 5000, MaxInvest = 500000000, PaymentFactor = 0.0052, CreateDate = DateTime.Now, CreateUser = adminUser
                    },
                    new()
                    {
                        Title = "پوشش دندانپزشکی", MinInvest = 4000, MaxInvest = 400000000, PaymentFactor = 0.0042, CreateDate = DateTime.Now, CreateUser = adminUser
                    },
                    new()
                    {
                        Title = "پوشش بستری", MinInvest = 2000, MaxInvest = 200000000, PaymentFactor = 0.005, CreateDate = DateTime.Now, CreateUser = adminUser
                    },
                });

                await dbContext.Persons.AddAsync(new()
                {
                    FirstName = "علی",
                    LastName = "احمدی",
                    CreateDate = DateTime.Now,
                    CreateUser = adminUser
                });

                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred via initailizing database.");
        }
    }
}
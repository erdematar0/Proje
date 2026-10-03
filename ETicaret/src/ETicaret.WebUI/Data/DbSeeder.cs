using ETicaret.DataAccess.Context;
using ETicaret.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.WebUI.Data;

public static class DbSeeder
{
    public const string AdminRole = "Admin";
    public const string CustomerRole = "Customer";

    public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in new[] { AdminRole, CustomerRole })
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var email = config["AdminUser:Email"]!;
        if (await userManager.FindByEmailAsync(email) is null)
        {
            var admin = new AppUser
            {
                UserName = email, Email = email, EmailConfirmed = true,
                FirstName = "Site", LastName = "Yöneticisi"
            };
            var result = await userManager.CreateAsync(admin, config["AdminUser:Password"]!);
            if (result.Succeeded) await userManager.AddToRoleAsync(admin, AdminRole);
        }
    }
}

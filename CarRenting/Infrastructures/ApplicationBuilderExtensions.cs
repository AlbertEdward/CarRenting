using CarRenting.Data;
using CarRenting.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


using static CarRenting.Infrastructures.Seeder.Roles;

namespace CarRenting.Infrastructures
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PreparedDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var service = serviceScope.ServiceProvider;

            MigrateDatabase(service);

            SeedCategories(service);
            SeedAdministrator(service);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider service)
        {
            var data = service.GetRequiredService<CarRentingDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarRentingDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Economy"},
                new Category { Name = "Comfort"},
                new Category { Name = "Premium"},
                new Category { Name = "Elite"},
                new Category { Name = "SUV"},
                new Category { Name = "Minivan"},
                new Category { Name = "Cargo"},
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(Administrator))
                {
                    return;
                }

                var role = new IdentityRole { Name = Administrator };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@abv.bg";
                const string adminPassword = "123456";

                var applicationUser = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(applicationUser, adminPassword);

                await userManager.AddToRoleAsync(applicationUser, role.Name);

            })
            .GetAwaiter()
            .GetResult();
        }
    }
}

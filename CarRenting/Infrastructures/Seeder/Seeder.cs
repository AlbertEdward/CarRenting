using CarRenting.Data;
using Microsoft.AspNetCore.Identity;

namespace CarRenting.Infrastructures.Seeder
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<CarRentingDbContext>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            await context.SeedAsync(roleManager);
        }

        private static async Task SeedAsync(this CarRentingDbContext context, RoleManager<IdentityRole> roleManager)
        {
            await context.AddRolesAsync(roleManager);

            await context.SaveChangesAsync();
        }
    }
}

using CarRenting.Data;
using Microsoft.AspNetCore.Identity;

namespace CarRenting.Infrastructures.Seeder
{
    public static class RolesSeeder
    {
        public static async Task<CarRentingDbContext> AddRolesAsync(this CarRentingDbContext context, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Administrator))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator));
            }

            if (!await roleManager.RoleExistsAsync(Roles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }

            return context;
        }
    }
}

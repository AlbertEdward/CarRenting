using CarRenting.Data;
using CarRenting.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRenting.Infrastructures
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PreparedDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServises = app.ApplicationServices.CreateScope();

            var data = scopedServises.ServiceProvider.GetService<CarRentingDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(CarRentingDbContext data)
        {
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
    }
}

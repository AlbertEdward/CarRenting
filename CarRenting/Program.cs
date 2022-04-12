using CarRenting.Data;
using CarRenting.Data.Models;
using CarRenting.Infrastructures;
using CarRenting.Services.Cars;
using CarRenting.Services.Statistics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = @"Server=.;Database=CarRenting;Integrated Security=True;";
builder.Services.AddDbContext<CarRentingDbContext>(options => options
.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarRentingDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<ICarService, CarService>();

var app = builder.Build();

app.PreparedDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
    });
app.UseAuthentication();
app.Run();

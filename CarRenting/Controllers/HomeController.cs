using System.Diagnostics;
using CarRenting.Data;
using CarRenting.Models;
using CarRenting.Models.Home;
using CarRenting.Services.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class HomeController : Controller
    {
        public int activeIndex;
        
        private readonly CarRentingDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(IStatisticsService statistics, CarRentingDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
            this.activeIndex = 0;
        }

        public IActionResult Index()
        {
            var totalCars = this.data.Cars.Count();
            var totalUser = this.data.Users.Count();

            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description
                })
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();


            return View(new IndexViewModel
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                Cars = cars,
                ActiveIndex = activeIndex

            });
        }

        public void OnCarouselNavClick(int index)
        {
            this.activeIndex = index;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
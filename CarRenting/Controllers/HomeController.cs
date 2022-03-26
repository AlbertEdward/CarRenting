using System.Diagnostics;
using CarRenting.Data;
using CarRenting.Models;
using CarRenting.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarRentingDbContext data;

        public HomeController(CarRentingDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalCars = this.data.Cars.Count();

            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexViewModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl
                })
                .Take(3)
                .ToList();

            return View(new IndexViewModel
            {
                TotalCars = totalCars,
                Cars = cars
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
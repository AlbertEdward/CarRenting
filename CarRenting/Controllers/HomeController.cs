using System.Diagnostics;
using CarRenting.Data;
using CarRenting.Models;
using CarRenting.Models.Cars;
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

                var cars = this.data
                    .Cars
                    .OrderByDescending(c => c.Id)
                    .Select(car => new CarListingViewModel
                    {
                        Id = car.Id,
                        Make = car.Make,
                        Model = car.Model,
                        Year = car.Year,
                        ImageUrl = car.ImageUrl,
                        Category = car.Category.Name
                    })
                    .Take(3)
                    .ToList();

                return View(cars);
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
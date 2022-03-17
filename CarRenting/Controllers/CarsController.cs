using CarRenting.Data;
using CarRenting.Models.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;

        public CarsController(CarRentingDbContext data)
        {
            this.data=data;
        }

        public IActionResult Add() => View(new AddCarFormModel
        {
            Categories = this.GetCarCategories()
        });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            return View();
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories() 
            => this.data
                .Categories
                .Select(c => new CarCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}

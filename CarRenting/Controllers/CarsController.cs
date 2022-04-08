using CarRenting.Data;
using CarRenting.Data.Models;
using CarRenting.Models.Cars;
using CarRenting.Services.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;
        private readonly ICarService carService;

        public CarsController(CarRentingDbContext data, ICarService carService)
        {
            this.data=data;
            this.carService = carService;
        }

        [Authorize]
        public IActionResult Add() => View(new CarFormModel
        {
            Categories = this.GetCarCategories()
        });

        public IActionResult All([FromQuery]AllCarsQueryModel query)
        {
            var queryResult = this.carService.AllCarsInactive(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            var carBrands = this.carService.AllCarBrands();

            query.TotalCars = queryResult.TotalCars;
            query.Brands = carBrands;
            query.Cars = queryResult.Cars;

            return View(query);
        }
        public IActionResult RentNow([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.carService.AllActiveCars(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            var carBrands = this.carService.AllCarBrands();

            query.TotalCars = queryResult.TotalCars;
            query.Brands = carBrands;
            query.Cars = queryResult.Cars;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            if (!this.data.Categories.Any(c=>c.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.GetCarCategories();
                return View(car);
            }

            var carData = new Car
            {
                Make = car.Make,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId,
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
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

        public IActionResult Delete(int id)
        {
            var car = this.data.Cars.FirstOrDefault(c => c.Id == id);

            data.Cars.Remove(car);

            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

    }
}

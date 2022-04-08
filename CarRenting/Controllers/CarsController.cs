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
        private readonly ICarService cars;

        public CarsController(CarRentingDbContext data, ICarService cars)
        {
            this.data=data;
            this.cars=cars;
        }

        [Authorize]
        public IActionResult Add() => View(new CarFormModel
        {
            Categories = this.GetCarCategories()
        });

        public IActionResult All([FromQuery]AllCarsQueryModel query)
        {
            var queryResult = this.cars.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            var carBrands = this.cars.AllCarBrands();

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

        //public CarDetailsModel Details(int id)
        //    => this.data
        //    .Cars
        //    .Where(c => c.Id == id)
        //    .Select(c => new CarDetailsModel
        //    {
        //        Make = c.Make,
        //        Model = c.Model,
        //        Description = c.Description,
        //        ImageUrl = c.ImageUrl,
        //        Year = c.Year,
        //        Category = c.Category.Name,
        //        IsActive = c.IsActive
        //    })
        //    .FirstOrDefault();
    }
}

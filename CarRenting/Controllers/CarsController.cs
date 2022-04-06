using CarRenting.Data;
using CarRenting.Data.Models;
using CarRenting.Models.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;

        public CarsController(CarRentingDbContext data) => this.data=data;
 
        [Authorize]
        public IActionResult Add() => View(new CarFormModel
        {
            Categories = this.GetCarCategories()
        });

        public IActionResult All([FromQuery]AllCarsQueryModel query)
        {
            var queryString = Request.Query;
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Make == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    c.Make.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Model.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Make).ThenBy(c => c.Model),
                CarSorting.DateCreated or  _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((query.CurrentPage - 1) * AllCarsQueryModel.CarsPerPage)
                .Take(AllCarsQueryModel.CarsPerPage)
                .OrderByDescending(c => c.Id)
                .Select(car => new CarListingViewModel
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    ImageUrl = car.ImageUrl,
                    Category = car.Category.Name,
                    IsActive = car.IsActive
                })
                .ToList();

            var carBrands = this.data
                .Cars
                .Select(c => c.Make)
                .Distinct()
                .ToList();

            query.TotalCars = totalCars;
            query.Brands = carBrands;
            query.Cars = cars;

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

            //this.cars.Create(
            //    car.Make,
            //    car.Model,
            //    car.Description,
            //    car.ImageUrl,
            //    car.Year,
            //    car.CategoryId,
            //    car.IsActive); 
            
            return RedirectToAction(nameof(All));
        }

        //[Authorize]
        //public IActionResult Edit(int id)
        //{
        //    var car = this.cars.Details(id);

        //    return View(new CarFormModel
        //    {
        //        Make = car.Make,
        //        Model = car.Model,
        //        Description = car.Description,
        //        ImageUrl = car.ImageUrl,
        //        Year = car.Year,
        //        CategoryId = car.CategoryId,
        //        IsActive = car.IsActive
        //    });
        //}

        private IEnumerable<CarCategoryViewModel> GetCarCategories() 
            => this.data
                .Categories
                .Select(c => new CarCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public CarDetailsModel Details(int id)
            => this.data
            .Cars
            .Where(c => c.Id == id)
            .Select(c => new CarDetailsModel
            {
                Make = c.Make,
                Model = c.Model,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Year = c.Year,
                Category = c.Category.Name,
                IsActive = c.IsActive
            })
            .FirstOrDefault();
    }
}

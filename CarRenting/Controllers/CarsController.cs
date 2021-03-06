using CarRenting.Data;
using CarRenting.Models.Cars;
using CarRenting.Services.Cars;
using CarRenting.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CarRenting.WebConstants;

namespace CarRenting.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;
        private readonly ICarService carService;
        private readonly IOrderService orderService;

        public CarsController(CarRentingDbContext data, ICarService carService, IOrderService orderService)
        {
            this.data = data;
            this.carService = carService;
            this.orderService = orderService;
        }
        
        [Authorize]
        public IActionResult Add() => View(new CarFormModel
        {
            Categories = this.carService.AllCategories()
        });

        public IActionResult Details(int id)
        {
            var car = this.carService.Details(id);

            return View(new CarFormModel
            {
                Id = id,
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                IsActive = car.IsActive,
                Categories = this.carService.AllCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder(CarFormModel car)
        {
            TempData[GlobalMessage] = "Placed Order!";

            var order = this.orderService.CreateOrder(car.Id);

            return RedirectToAction("Index", "Orders");
        }

        public IActionResult All([FromQuery]AllCarsQueryModel query)
        {
            var queryResult = this.carService.AllInactive(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            var carBrands = this.carService.AllBrands();

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

            var carBrands = this.carService.AllBrands();

            query.TotalCars = queryResult.TotalCars;
            query.Brands = carBrands;
            query.Cars = queryResult.Cars;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            if (!this.carService.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.carService.AllCategories();

                return View(car);
            }

            this.carService.Create(
                car.Brand,
                car.Model,
                car.Description,
                car.ImageUrl,
                car.Year,
                car.CategoryId,
                car.IsActive);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var car = this.data.Cars.FirstOrDefault(c => c.Id == id);

            data.Cars.Remove(car);

            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var car = this.carService.Details(id);

            return View(new CarFormModel
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                IsActive = car.IsActive,
                Categories = this.carService.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CarFormModel car)
        {
            if (!this.carService.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.carService.AllCategories();

                return View(car);
            }

            var carIsEdited = this.carService.Edit(
                id,
                car.Brand,
                car.Model,
                car.Description,
                car.ImageUrl,
                car.Year,
                car.CategoryId,
                car.IsActive);

            if (!carIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}

using CarRenting.Data;
using CarRenting.Data.Models;
using CarRenting.Models.Cars;

namespace CarRenting.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly CarRentingDbContext data;

        public CarService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public CarQueryServiceModel AllActiveCars(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    c.Brand.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Model.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or  _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .OrderByDescending(c => c.Id)
                .Where(c => c.IsActive == true)
                .Select(car => new CarServiceModel
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    ImageUrl = car.ImageUrl,
                    CategoryName = car.Category.Name,
                    IsActive = car.IsActive
                })
                .ToList();

            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }

        public IEnumerable<string> AllBrands()
            => this.data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .ToList();

        public IEnumerable<CarCategoryServiceModel> AllCategories()
            => this.data
                .Categories
                .Select(c => new CarCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public CarQueryServiceModel AllInactive(string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage)
        
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    c.Brand.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Model.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or  _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .OrderByDescending(c => c.Id)
                .Select(car => new CarServiceModel
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    ImageUrl = car.ImageUrl,
                    CategoryName = car.Category.Name,
                    IsActive = car.IsActive
                })
                .ToList();

            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }

        public bool CategoryExists(int categoryId)
            => this.data
                .Categories
                .Any(c => c.Id == categoryId);

        public int Create(string make, string model, string description, string imageUrl, int year, int categoryId, bool isActive)
        {
            var carData = new Car
            {
                Brand = make,
                Model = model,
                Description = description,
                ImageUrl = imageUrl,
                Year = year,
                CategoryId = categoryId,
                IsActive = isActive
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return carData.Id;
        }

        public bool Edit(int id, string make, string model, string description, string imageUrl, int year, int categoryId, bool isActive)
        {
            var carData = this.data.Cars.Find(id);

            //TODO Add AdminRole who is alowed to edit cars.

            if (carData == null)
            {
                return false;
            }

            carData.Brand = make;
            carData.Model = model;
            carData.Description = description;
            carData.ImageUrl = imageUrl;
            carData.Year = year;
            carData.CategoryId = categoryId;
            carData.IsActive = isActive;

            this.data.SaveChanges();

            return true;
        }

        public CarDetailsServiceModel Details(int id)
        => this.data
            .Cars
            .Where(c => c.Id == id)
            .Select(car => new CarDetailsServiceModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                Year = car.Year,
                ImageUrl = car.ImageUrl,
                CategoryName = car.Category.Name,
                IsActive = car.IsActive
            })
            .FirstOrDefault();
    }
}

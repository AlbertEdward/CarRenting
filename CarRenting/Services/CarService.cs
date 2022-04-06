using CarRenting.Data;
using CarRenting.Data.Models;

namespace CarRenting.Services
{
    public class CarService : ICarService
    {
        private readonly CarRentingDbContext data;

        public CarService(CarRentingDbContext data)
            => this.data = data;

        public int Create(
            string make, 
            string model, 
            string description, 
            string imageUrl, 
            int year, 
            int categoryId, 
            bool isActive)
        {
            var carData = new Car
            {
                Make = make,
                Model = model,
                Description = description,
                ImageUrl = imageUrl,
                Year = year,
                CategoryId = categoryId,
                IsActive = isActive
            };

            data.Cars.Add(carData);
            this.data.SaveChanges();

            return carData.Id;
        }
    }
}

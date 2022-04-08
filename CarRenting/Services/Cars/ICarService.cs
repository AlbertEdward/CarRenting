using CarRenting.Models.Cars;

namespace CarRenting.Services.Cars
{
    public interface ICarService
    {
        CarQueryServiceModel AllActiveCars(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);

        IEnumerable<string> AllCarBrands();

        CarQueryServiceModel AllCarsInactive(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);
    }
}

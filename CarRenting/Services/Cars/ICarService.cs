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

        CarDetailsServiceModel Details(int id);

        int Create(
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId,
            bool isActive);

        IEnumerable<string> AllBrands();

        CarQueryServiceModel AllInactive(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);

        IEnumerable<CarCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);

    }
}

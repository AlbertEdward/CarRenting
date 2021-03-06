using CarRenting.Services.Cars;
using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models.Cars
{
    public class AllCarsQueryModel
    {
        public const int CarsPerPage = 6;
        public string Brand { get; set; }
        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public string Description { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCars { get; set; }

        public bool IsActive { get; set; } = true;

        public CarSorting Sorting { get; init; }

        public IEnumerable<CarServiceModel> Cars { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models.Cars
{
    public class AllCarsQueryModel
    {
        public string Brand { get; set; }
        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }

        public IEnumerable<CarListingViewModel> Cars { get; set; }
    }
}

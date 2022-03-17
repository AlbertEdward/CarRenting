using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models.Cars
{
    public class AddCarFormModel
    {
        [Display(Name = "Brand")]
        public string Make { get; init; }

        public string Model { get; init; }

        public string Description { get; init; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; init; }

        public int Year { get; init; }

        [Display(Name ="Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}

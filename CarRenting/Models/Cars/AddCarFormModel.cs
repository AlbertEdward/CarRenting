using System.ComponentModel.DataAnnotations;
using static CarRenting.Data.DataConstants;

namespace CarRenting.Models.Cars
{
    public class AddCarFormModel
    {
        [Required]
        [StringLength(CarMakeMaxLength)]
        [Display(Name = "Brand")]
        public string Make { get; init; }

        [Required]
        [StringLength(CarModelMaxLength)]
        public string Model { get; init; }

        [Required]
        [StringLength(CarDescriptionMaxLength, MinimumLength = CarDescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; init; }

        [Required]
        public bool IsActive { get; set; } = true;

        public int Year { get; init; }

        [Display(Name ="Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}

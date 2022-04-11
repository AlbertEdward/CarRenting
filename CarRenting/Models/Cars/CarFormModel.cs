using System.ComponentModel.DataAnnotations;
using CarRenting.Services.Cars;
using static CarRenting.Data.DataConstants.Car;


namespace CarRenting.Models.Cars
{
    public class CarFormModel
    {
        [Required]
        [StringLength(CarMakeMaxLength)]
        public string Brand { get; init; }

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

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }
    }
}

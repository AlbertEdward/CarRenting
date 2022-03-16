using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models.Cars
{
    public class AddCarFormModel
    {
        public int Id { get; init; }

        [Display(Name ="Brand")]
        public string Make { get; init; }

        public string Model { get; init; }

        public string Description { get; init; }

        [Display(Name ="Image Url")]
        public string ImageUrl { get; init; }

        public int Year { get; init; }

        public int CategoryId { get; init; }
    }
}

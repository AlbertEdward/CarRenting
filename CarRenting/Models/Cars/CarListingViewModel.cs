namespace CarRenting.Models.Cars
{
    public class CarListingViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public bool IsActive { get; set; }

        public string Category { get; set; }
    }
}

namespace CarRenting.Services.Cars
{
    public class CarServiceModel
    {
        public int Id { get; init; }

        public string Make { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }

        public bool IsActive { get; init; }

        public string Category { get; init; }
    }
}

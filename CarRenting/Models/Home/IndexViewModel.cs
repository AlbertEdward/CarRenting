namespace CarRenting.Models.Home
{
    public class IndexViewModel
    {
        public int TotalCars { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRents { get; init; }

        public int ActiveIndex { get; set; }

        public List<CarIndexViewModel> Cars { get; init; }
    }
}

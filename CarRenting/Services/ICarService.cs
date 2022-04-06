namespace CarRenting.Services
{
    public interface ICarService
    {
        int Create(
            string make,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId,
            bool isActive);
    }
}

using CarRenting.Data;
using CarRenting.Data.Models;
using CarRenting.Services.Orders;
using static CarRenting.WebConstants;

namespace CarRenting.Services
{
    public class OrderService : IOrderService
    {
        private readonly CarRentingDbContext data;

        public OrderService(CarRentingDbContext data)
        {
            this.data=data;
        }

        public int CreateOrder(int carId)
        {
            var orderData = new Order
            {
                carId = carId,
            };

            this.data.Orders.Add(orderData);
            this.data.SaveChanges();

            return orderData.Id;
        }
    }
}

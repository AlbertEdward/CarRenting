using System.ComponentModel.DataAnnotations;

namespace CarRenting.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int carId { get; set; }

        public Car Car { get; set; }

    }
}

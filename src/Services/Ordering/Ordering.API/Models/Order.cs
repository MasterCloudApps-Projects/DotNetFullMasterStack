using System.Collections.Generic;

namespace Ordering.API.Models
{
    public class Order
    {
        public int CustomerId { get; set; }
        public Address DeliveryAddress { get; set; }
        public int Id { get; set; }
        public List<OrderLine> OrderDetails { get; set; }
        public int RestaurantId { get; set; }
    }
}
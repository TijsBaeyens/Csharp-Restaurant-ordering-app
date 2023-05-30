using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models {
    public class OrderDish {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Amount { get; set; }
        public OrderDish() { }
        public OrderDish(int orderId, int dishId, int amount) {
            OrderId = orderId;
            DishId = dishId;
            Amount = amount;
        }
        public OrderDish(int id, int orderId, int dishId, int amount) {
            Id = id;
            OrderId = orderId;
            DishId = dishId;
            Amount = amount;
        }
    }
}

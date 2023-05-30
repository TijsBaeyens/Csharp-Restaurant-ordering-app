using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces {
    public interface IMenuRepo {
        // USER SECTION
        public void UpdateUser(User user);
        public void DeleteUser(int id);
        public List<User> GetAllUsers();
        public void CreateUser(User a, byte[] salt);

        // DISH SECTION
        public void AddDish(string name, string description, int price, int amountAvailible);
        public void UpdateDish(int id, string name, string description, int price, int amountAvailible);
        public void DeleteDish(int id);
        public List<Dish> GetAllDishes();
        public Dish GetDishById(int id);
        public Dish GetDishByName(string name);
        public void UpdateDishAmount(int id, int amount);

        // ORDER SECTION
        public void CreateOrder(int userId);
        public void AddDishToOrder(int orderId, int dishId, int quantity);
        public Order GetActiveOrder(int userId);
        public void PayActiveOrder(int userId);
        public void GetAllBesteldeGerechten(int orderId);
        public List<Order> GetAllOrders();

        // ORDERDISH SECTION
        public void AddOrderDish(int orderId, int dishId, int amount);
        public void UpdateOrderDish(int orderId, int dishId, int amount);
        public void DeleteOrderDish(int orderId, int dishId);
        public List<OrderDish> GetAllOrderDishes();
        public List<OrderDish> GetOrderDishesByOrderId(int orderId);
        public void DeleteAllOrderDish(int orderId);

    }
}

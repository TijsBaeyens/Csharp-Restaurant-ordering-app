using DomainLayer.Interfaces;
using DomainLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Controller {
    public class DomainController
    {
        private IMenuRepo _menuRepo;
        public DomainController(IMenuRepo mr)
        {
            _menuRepo = mr;
        }

        // USER ACCOUNT MANAGEMENT
        public void CreateUser(string lastName, string firstName, string street, int houseNumber, string city, int postalCode, string country, string phoneNumber, string emailAdress, string password, string busNumber)
        {
            try
            {
                List<User> users = _menuRepo.GetAllUsers();
                foreach (User user in users)
                {
                    if (user.EmailAddress == emailAdress)
                    {
                        throw new Exception("Email already in use");
                    }
                    if (user.PhoneNumber == phoneNumber)
                    {
                        throw new Exception("Phone number already in use");
                    }
                }
                byte[] salt = GenerateSalt();
                string hashedPassword = HashPassword(password, salt);
                User a = new User(firstName, lastName, street, city, houseNumber, busNumber, postalCode, country, emailAdress, phoneNumber, hashedPassword, salt);
                _menuRepo.CreateUser(a, salt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; // 16 bytes = 128 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // Hash the password with the provided salt using the bcrypt algorithm
        static string HashPassword(string password, byte[] salt)
        {
            using (var bcrypt = new Rfc2898DeriveBytes(password, salt, 10000)) // 10000 iterations
            {
                byte[] hash = bcrypt.GetBytes(20); // 20 bytes = 160 bits
                byte[] hashBytes = new byte[36]; // 36 bytes = 288 bits
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string hashedPassword = Convert.ToBase64String(hashBytes);
                return hashedPassword;
            }
        }

        // Verify the entered password by comparing the hash of the entered password with the stored hash
        static bool VerifyPassword(string enteredPassword, string hashedPassword, byte[] salt, bool loginOrRegister)
        {
            if (loginOrRegister)
            {
                if (hashedPassword == HashPassword(enteredPassword, salt))
                {
                    return true;
                }
            }
            else
            {
                if (enteredPassword == hashedPassword)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUserById(int id)
        {
            try
            {
                List<User> users = _menuRepo.GetAllUsers();
                foreach (User usr in users)
                {
                    if (usr.Id == id)
                    {
                        return usr;
                    }
                }
                throw new Exception("User not found");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public User GetUserByEmail(string email)
        {
            try
            {
                List<User> users = _menuRepo.GetAllUsers();
                foreach (User usr in users)
                {
                    if (usr.EmailAddress.ToLower() == email.ToLower())
                    {
                        return usr;
                    }
                }
                throw new Exception("User not found");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void logInOrOut(string LastName, string password, bool loginOrRegister)
        {
            try
            {
                List<User> users = _menuRepo.GetAllUsers();
                foreach (User usr in users)
                {
                    if (usr.LastName == LastName && VerifyPassword(password, usr.Password, usr.Salt, loginOrRegister))
                    {
                        if (usr.Active)
                        {
                            usr.Active = false;
                            _menuRepo.UpdateUser(usr);
                            return;
                        }
                        else
                        {
                            usr.Active = true;
                            _menuRepo.UpdateUser(usr);
                            return;
                        }
                    }
                }
                throw new Exception("User not found");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateUser(int id, string name, string firstName, string street, int houseNumber, string city, int postalCode, string country, string phoneNumber, string emailAdress, string password, string busNumber, bool Active = false)
        {
            try
            {
                List<User> users = _menuRepo.GetAllUsers();
                foreach (User user in users)
                {
                    if (user.EmailAddress == emailAdress && user.Id != id)
                    {
                        throw new Exception("Email already in use");
                    }
                    if (user.PhoneNumber == phoneNumber && user.Id != id)
                    {
                        throw new Exception("Phone number already in use");
                    }
                }
                byte[] salt = GenerateSalt();
                User a = new User(firstName, name, street, city, houseNumber, busNumber, postalCode, country, emailAdress, phoneNumber, HashPassword(password, salt), salt, Active);
                a.Id = id;
                _menuRepo.UpdateUser(a);
                }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteUser(int id) {
            try {
                _menuRepo.DeleteUser(id);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }

        }

        public User GetActiveUser() {
            try {
                List<User> users = _menuRepo.GetAllUsers();
                foreach (User usr in users) {
                    if (usr.Active) {
                        return usr;
                    }
                }
                throw new Exception("No active user");
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        // Dish Management
        public List<Dish> GetAllAvailableDishes() {
            try {
                List<Dish> dishes = _menuRepo.GetAllDishes();
                List<Dish> availableDishes = new List<Dish>();
                foreach (Dish dish in dishes) {
                    if (dish.AvailableQuantity > 0) {
                        availableDishes.Add(dish);
                    }
                }
                return availableDishes;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        public Dish GetDishByName(string name) {
            try {
                List<Dish> dishes = _menuRepo.GetAllDishes();
                foreach (Dish dish in dishes) {
                    if (dish.Name.ToLower() == name.ToLower()) {
                        return dish;
                    }
                }
                throw new Exception("Dish not found");
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public Dish GetDishById(int id) {
            try {
                List<Dish> dishes = _menuRepo.GetAllDishes();
                foreach (Dish dish in dishes) {
                    if (dish.Id == id) {
                        return dish;
                    }
                }
                throw new Exception("Dish not found");
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        // ORDER MANAGEMENT

        public void CreateOrder(int userId) {
            try {
                Order order = new Order(userId);
                List<Order> orders = _menuRepo.GetAllOrders();
                foreach (Order ord in orders) {
                    if (ord.UserId == userId && ord.PaymentDateTime == DateTime.MinValue) {
                        throw new Exception("User already has an open order");
                    }
                }
                _menuRepo.CreateOrder(userId);
            } catch {
                throw new Exception("User already has an open order");
            }
        }

        public void AddOrderDish(int orderId, int dishId, int amount) {
            try {
                Dish dish = _menuRepo.GetDishById(dishId);
                if (dish == null) {
                    throw new Exception("This dish doesn't exist");
                }
                if (dish.AvailableQuantity >= amount) {
                    List<OrderDish> AllOrder = _menuRepo.GetAllOrderDishes();
                    bool alreadyExists = false;
                    int amountAlreadyInOrder = 0;
                    foreach (OrderDish dish1 in AllOrder) {
                        if (dish1.OrderId == orderId && dish1.DishId == dishId) {
                            alreadyExists = true;
                            amountAlreadyInOrder = dish1.Amount;
                            break;
                        }
                    }
                    if (alreadyExists) {
                        ChangeOrderDishAmount(orderId, dishId, (amount + amountAlreadyInOrder));
                    } else {
                        if (amount >= 0) {
                            _menuRepo.AddOrderDish(orderId, dishId, amount);
                        } else {
                            throw new Exception("Amount can not be 0 or lower");
                        }
                    }
                    _menuRepo.UpdateDishAmount(dishId, dish.AvailableQuantity - amount);
                } else {
                    throw new Exception("There isn't enough inventory to make this much, choose a lower amount of this product");
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public void DeleteAllOrderDish(int orderId) {
            try {
                List<OrderDish> orderDishes = _menuRepo.GetOrderDishesByOrderId(orderId);
                foreach (OrderDish orderDish in orderDishes) {
                    int amount = _menuRepo.GetDishById(orderDish.DishId).AvailableQuantity;
                    _menuRepo.UpdateDishAmount(orderDish.DishId, amount + orderDish.Amount);
                }
                _menuRepo.DeleteAllOrderDish(orderId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public void ChangeOrderDishAmount(int orderId, int dishId, int amount) {
            try {
                Dish dish = _menuRepo.GetDishById(dishId);
                List<OrderDish> orderDishes = _menuRepo.GetOrderDishesByOrderId(orderId);
                OrderDish order = new OrderDish();
                foreach (OrderDish od in orderDishes) {
                    if (od.OrderId == orderId) {
                        order = od;
                        break;
                    }
                }

                int updateAmount = 0;
                if (amount < order.Amount) {
                    updateAmount = dish.AvailableQuantity + (order.Amount - amount);
                } else if (amount > order.Amount) {
                    updateAmount = dish.AvailableQuantity - (amount - order.Amount);
                } else { updateAmount = dish.AvailableQuantity; }

                if (updateAmount < 0) {
                    throw new Exception("There isn't enough ingredients to make this many of this recipe, please choose something else");
                }
                _menuRepo.UpdateDishAmount(dishId, updateAmount);
                _menuRepo.UpdateOrderDish(orderId, dishId, amount);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public List<OrderDish> GetSpecificOrder(int userId) {
            try {
                int orderId;
                if (_menuRepo.GetActiveOrder(userId) != null) {
                    orderId = _menuRepo.GetActiveOrder(userId).Id;
                } else {
                    _menuRepo.CreateOrder(userId);
                    orderId = _menuRepo.GetActiveOrder(userId).Id;
                }
                List<OrderDish> allOrders = _menuRepo.GetAllOrderDishes();
                List<OrderDish> order = new List<OrderDish>();
                foreach (OrderDish item in allOrders) {
                    if (item.OrderId == orderId && item.Amount > 0) {
                        order.Add(item);
                    }
                }
                return order;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public void PayActiveOrder(int userId) {
            try {
                Order order = _menuRepo.GetActiveOrder(userId);
                if (order == null) {
                    throw new Exception("No active order");
                }
                _menuRepo.PayActiveOrder(userId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public Order GetActiveOrder(int userId) {
            try {
                Order order = _menuRepo.GetActiveOrder(userId);
                if (order == null) {
                    throw new Exception("No active order");
                }
                return order;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public void DeleteOrderDish(int orderId, int dishId) {
            try {
                _menuRepo.DeleteOrderDish(orderId, dishId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public List<Order> GetAllOrders()
        {
           try
            {
                return _menuRepo.GetAllOrders();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderDish> GetOrderById(int id)
        {
            try
            {
                return _menuRepo.GetOrderDishesByOrderId(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void CreateNewDish(string name, string description, int price, int amount)
        {
            try
            {
                _menuRepo.AddDish(name, description, price, amount);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateDish(int id, string name, string description, int price, int amount)
        {
            try
            {
                _menuRepo.UpdateDish(id, name, description, price, amount);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteDish(int id)
        {
            try
            {
                _menuRepo.DeleteDish(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable GetAllDishes()
        {
            try
            {
                return _menuRepo.GetAllDishes();
            } catch
            {
                   throw new Exception("Something went wrong");
            }
        }
    }
}

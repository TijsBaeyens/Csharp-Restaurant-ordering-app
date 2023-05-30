using DomainLayer.Controller;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PersistenceLayer;
using System.Collections;
using System.Collections.ObjectModel;
using WebApplication1.Mappers;
using WebApplication1.Models.DTO.Input;
using WebApplication1.Models.DTO.Output;

namespace WebApplication1.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private DomainController _controller;
        private IMenuRepo _menuRepo;
        private string _connectionString;
        private DishMapper _mapper;

        public UserController(string conn) {
            _connectionString = conn;
            _menuRepo = new MenuRepo(_connectionString);
            _controller = new DomainController(_menuRepo);
            _mapper = new DishMapper(this);
        }

        // USER ACCOUNT MANAGEMENT
        [HttpPost("user/CreateUser")]
        public void CreateUser(string lastname, string firstName, string street, int houseNumber, string city, int postalCode, string country, string phoneNumber, string emailAdress, string password, string busNumber = null) {
            try {
                _controller.CreateUser(lastname, firstName, street, houseNumber, city, postalCode, country, phoneNumber, emailAdress, password, busNumber);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("user/GetUserById")]
        public IActionResult GetUserById(int id) {
            try {
                User user = _controller.GetUserById(id);
                return Ok(user);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("user/GetUserByEmail")]
        public IActionResult GetUserByEmail(string email) {
            try {
                User user = _controller.GetUserByEmail(email);
                return Ok(user);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("user/logInOrOut")]
        public void logInOrOut(string lastName, string password, bool loginOrRegister) {
            try {
                _controller.logInOrOut(lastName, password, loginOrRegister);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpPost("user/UpdateUser")]
        public void UpdateUser(int id, string name, string firstName, string street, int houseNumber, string city, int postalCode, string country, string phoneNumber, string emailAdress, string password, string busNumber = null, bool Active = false) {
            try {
                _controller.UpdateUser(id, name, firstName, street, houseNumber, city, postalCode, country, phoneNumber, emailAdress, password, busNumber, Active);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete("user/DeleteUser")]
        public void DeleteUser(int id) {
            try {
                _controller.DeleteUser(id);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("user/GetActiveUser")]
        public User GetActiveUser() {
            try {
                User user = _controller.GetActiveUser();
                return user;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        // DISH MANAGEMENT
        [HttpGet("dish/GetAllAvailableDishes")]
        public ObservableCollection<DishInputDTO> GetAllAvailableDishes() {
            try {
                List<Dish> dishes = _controller.GetAllAvailableDishes();
                ObservableCollection<DishInputDTO> dishesDTO = new ObservableCollection<DishInputDTO>();
                foreach (Dish dish in dishes) {
                    dishesDTO.Add(_mapper.MapToDTO(dish));
                }
                return dishesDTO;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("dish/GetDishById")]
        public Dish GetDishById(int id) {
            try {
                Dish dish = _controller.GetDishById(id);
                return dish;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("dish/GetDishByName")]
        public Dish GetDishByName(string name) {
            try {
                Dish dish = _controller.GetDishByName(name);
                return dish;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        // ORDER MANAGEMENT
        [HttpPost("order/CreateOrder")]
        public void CreateOrder(int userId) {
            try {
                _controller.CreateOrder(userId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        [HttpPost("order/AddOrderDish")]
        public void AddOrderDish(int orderId, int dishId, int amount) {
            try {
                _controller.AddOrderDish(orderId, dishId, amount);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete("order/DeleteOrderDish")]
        public void DeleteAllOrderDish(int orderId) { 
            try {
                _controller.DeleteAllOrderDish(orderId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete("order/DeleteOrderDish")]
        public void DeleteOrderDish(int orderId, int dishId) {
            try {
                _controller.DeleteOrderDish(orderId, dishId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        [HttpPost("order/ChangeOrderDishAmount")]
        public void ChangeOrderDishAmount(int orderId, int dishId, int amount) {
            try {
                _controller.ChangeOrderDishAmount(orderId, dishId, amount);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("order/GetSpecificOrder")]
        public List<OrderDish> GetSpecificOrder(int userId) {
            try {
                return _controller.GetSpecificOrder(userId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpPost("order/PayOrder")]
        public void PayOrder(int userId) {
            try {
                _controller.PayActiveOrder(userId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("order/GetActiveOrder")]
        public Order GetActiveOrder(int userId) {
            try {
                return _controller.GetActiveOrder(userId);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("order/GetDishesDTO")]
        public ObservableCollection<OrderDishInputDTO> GetSpecificOrderInDTO(int id) {
            try {
                List<OrderDish> dishes = _controller.GetSpecificOrder(id);
                ObservableCollection<OrderDishInputDTO> dishesDTO = new ObservableCollection<OrderDishInputDTO>();
                foreach (OrderDish dish in dishes) {
                    dishesDTO.Add(_mapper.MapOrderDishInputToDTO(dish));
                }
                return dishesDTO;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("order/GetAllOrders")]
        public ObservableCollection<OrderInputDTO> GetAllOrders(int userId)
        {
            try
            {
                List<Order> orders = _controller.GetAllOrders();
                ObservableCollection<OrderInputDTO> ordersDTO = new ObservableCollection<OrderInputDTO>();
                foreach (Order order in orders)
                {
                    if (order.UserId == userId && order.PaymentDateTime != DateTime.MinValue)
                    {
                        ordersDTO.Add(_mapper.MapOrderToDTO(order));
                    }
                }
                return ordersDTO;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("order/GetOrderById")]

        public IEnumerable<OrderDish> GetOrderById(int id)
        {
            try
            {
                return _controller.GetOrderById(id);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost("dish/CreateNewDish")]
        public void CreateNewDish(string name, string description, int price, int amount)
        {
            try
            {
                _controller.CreateNewDish(name, description, price, amount);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut("dish/UpdateDish")]
        public void UpdateDish(int id, string name, string description, int price, int amount)
        {
            try
            {
                _controller.UpdateDish(id, name, description, price, amount);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete("dish/DeleteDish")]
        public void DeleteDish(int id)
        {
            try
            {
                _controller.DeleteDish(id);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("dish/GetAllDishes")]
        public IEnumerable GetAllDishes()
        {
            try
            {
                   return _controller.GetAllDishes();
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("heartbeat")]
        public bool Heartbeat()
        {
            bool isAvailable = IsDbContextAvailable();
            if (isAvailable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsDbContextAvailable()
        {
            try
            {
                _controller.GetAllDishes();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

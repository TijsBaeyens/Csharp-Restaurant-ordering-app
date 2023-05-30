using DomainLayer.Models;
using WebApplication1.Controllers;
using WebApplication1.Models.DTO.Input;

namespace WebApplication1.Mappers {
    public class DishMapper : Dish {
        private UserController _userController;

        public DishMapper(UserController us) {
            _userController = us;
        }

        public DishInputDTO MapToDTO(Dish dish) {
            DishInputDTO dishDto = new DishInputDTO {
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                AvailableQuantity = dish.AvailableQuantity
            };
            return dishDto;
        }

        public OrderDishInputDTO MapOrderDishInputToDTO(OrderDish dish) {
            Dish dish1 = _userController.GetDishById(dish.DishId);
            OrderDishInputDTO dishDto = new OrderDishInputDTO {
                DishName = dish1.Name,
                DishDescription = dish1.Description,
                Quantity = dish.Amount,
                TotalPrice = (int)(dish.Amount * dish1.Price)
            };
            return dishDto;
        }

        public OrderInputDTO MapOrderToDTO(Order order)
        {
            decimal totaal = 0;
            foreach (OrderDish item in _userController.GetOrderById(order.Id))
            {
                totaal += item.Amount * _userController.GetDishById(item.DishId).Price;
            }

            OrderInputDTO orderDto = new OrderInputDTO
            {
                CreatedDateTime = order.CreatedDateTime,
                PaymentDateTime = order.PaymentDateTime,
                TotalPrice = totaal,
                Id = System.Guid.NewGuid()
            };
            return orderDto;
        }
    }
}

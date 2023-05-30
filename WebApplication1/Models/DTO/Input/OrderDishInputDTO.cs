namespace WebApplication1.Models.DTO.Input
{
    public class OrderDishInputDTO {
        public string DishName { get; set; }
        public string DishDescription {get; set;}
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}

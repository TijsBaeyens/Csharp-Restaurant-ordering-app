namespace WebApplication1.Models.DTO.Output {
    public class DishOutputDTO {
        public DishOutputDTO(string name, string description, decimal price, int availableQuantity) {
            Name = name;
            Description = description;
            Price = price;
            AvailableQuantity = availableQuantity;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
    }
}

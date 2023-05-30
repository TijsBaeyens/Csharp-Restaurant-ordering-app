namespace DomainLayer.Models {
    public class Dish {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public Dish() { }
        public Dish(string name, string description, int price, int amountAvailible) {
            Name = name;
            Description = description;
            Price = price;
            AvailableQuantity = amountAvailible;
        }
        public Dish(int id, string name, string description, int price, int amountAvailible) {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            AvailableQuantity = amountAvailible;
        }
    }
}

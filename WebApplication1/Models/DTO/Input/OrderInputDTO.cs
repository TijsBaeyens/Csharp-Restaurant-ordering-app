namespace WebApplication1.Models.DTO.Input
{
    public class OrderInputDTO {
        public DateTime CreatedDateTime { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid Id { get; set; }
    }
}

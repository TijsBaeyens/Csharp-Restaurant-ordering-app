namespace WebApplication1.Models.DTO.Input {
    public class UserInputDTO {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int HouseNumber { get; set; }
        public string BusNumber { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}

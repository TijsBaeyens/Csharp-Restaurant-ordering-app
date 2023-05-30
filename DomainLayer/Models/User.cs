using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models {
    public class User {
        public User() {
        }

        public User(string firstName, string lastName, string street, string city, int houseNumber, string busNumber, int postalCode, string country, string email, string phoneNumber, string password, byte[] salt, bool active = false) {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            HouseNumber = houseNumber;
            BusNumber = busNumber;
            PostalCode = postalCode;
            Country = country;
            EmailAddress = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Active = active;
            Salt = salt;
        }

        public User(int id, string firstName, string lastName, string street, string city, int houseNumber, string busNumber, int postalCode, string country, string email, string phoneNumber, string password, byte[] salt, bool active = false) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            HouseNumber = houseNumber;
            BusNumber = busNumber;
            PostalCode = postalCode;
            Country = country;
            EmailAddress = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Active = active;
            Salt = salt;
        }

        public int Id { get; set; }
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
        public byte[] Salt { get; set; }
    }
}

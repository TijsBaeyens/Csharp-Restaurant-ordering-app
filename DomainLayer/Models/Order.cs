using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models {
    public class Order {
        public Order() {
        }

        public Order(int userId) {
            UserId = userId;
        }

        public Order(int userId, DateTime createdDateTime, DateTime paymentDateTime) {
            UserId = userId;
            CreatedDateTime = createdDateTime;
            PaymentDateTime = paymentDateTime;
        }

        public Order(int id, int userId, DateTime createdDateTime, DateTime paymentDateTime) {
            Id = id;
            UserId = userId;
            CreatedDateTime = createdDateTime;
            PaymentDateTime = paymentDateTime;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime PaymentDateTime { get; set; }

    }
}

using DomainLayer.Interfaces;
using DomainLayer.Models;
using System.Data;
using System.Data.SqlClient;

namespace PersistenceLayer {
    public class MenuRepo : IMenuRepo {
        // SETUP SECTION
        private string _connectionString;
        public MenuRepo(string connectionString) {
            _connectionString = connectionString;
        }

        // USER SECTION
        public void CreateUser(User user, byte[] salt) {
            try {
                string sql = "INSERT INTO [User] (LastName, FirstName, Street, HouseNumber, City, PostalCode, Country, PhoneNumber, EmailAddress, PasswordHash, BusNumber, Active, Salt) VALUES (@Name, @FirstName, @Street, @HouseNumber, @City, @PostalCode, @Country, @PhoneNumber, @EmailAddress, @PasswordHash, @BusNumber, @Active, @Salt)";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = user.LastName;
                        command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = user.FirstName;
                        command.Parameters.Add("@Street", SqlDbType.VarChar, 50).Value = user.Street;
                        command.Parameters.Add("@HouseNumber", SqlDbType.Int).Value = user.HouseNumber;
                        command.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = user.City;
                        command.Parameters.Add("@PostalCode", SqlDbType.Int).Value = user.PostalCode;
                        command.Parameters.Add("@Country", SqlDbType.VarChar, 50).Value = user.Country;
                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = user.PhoneNumber;
                        command.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = user.EmailAddress;
                        command.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 50).Value = user.Password;
                        command.Parameters.Add("@BusNumber", SqlDbType.VarChar, 50).Value = user.BusNumber;
                        command.Parameters.Add("@Active", SqlDbType.Int, 50).Value = 0;
                        command.Parameters.Add("@Salt", SqlDbType.VarBinary, 50).Value = salt;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }
        public List<User> GetAllUsers() {
            try {
                List<User> users = new List<User>();
                string sql = "SELECT * FROM [User]";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                User user = new User();
                                user.Id = (int)reader["Id"];
                                user.FirstName = (string)reader["FirstName"];
                                user.LastName = (string)reader["LastName"];
                                user.Country = (string)reader["Country"];
                                user.EmailAddress = (string)reader["EmailAddress"];
                                user.PhoneNumber = (string)reader["PhoneNumber"];
                                user.Password = (string)reader["PasswordHash"];
                                user.PostalCode = (int)reader["PostalCode"];
                                user.City = (string)reader["City"];
                                user.Street = (string)reader["Street"];
                                user.HouseNumber = (int)reader["HouseNumber"];
                                user.BusNumber = (string)reader["BusNumber"];
                                if ((int)reader["Active"] == 0) {
                                    user.Active = false;
                                } else {
                                    user.Active = true;
                                }
                                user.Salt = (byte[])reader["Salt"];
                                users.Add(user);
                            }
                        }
                    }
                }
                return users;
            } catch (SqlException e) {
                throw e;
            }
        }
        public void UpdateUser(User user) {
            try {
                string sql = "UPDATE [User] SET FirstName = @FirstName, LastName = @LastName, Street = @Street, City = @City, HouseNumber = @HouseNumber, BusNumber = @BusNumber, PostalCode = @PostalCode, Country = @Country, PhoneNumber = @PhoneNumber, EmailAddress = @EmailAddress, PasswordHash = @PasswordHash, Active = @Active, Salt = @Salt WHERE Id = @Id";
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                            command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = user.FirstName;
                            command.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = user.LastName;
                            command.Parameters.Add("@Street", SqlDbType.VarChar, 50).Value = user.Street;
                            command.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = user.City;
                            command.Parameters.Add("@HouseNumber", SqlDbType.Int).Value = user.HouseNumber;
                            command.Parameters.Add("@BusNumber", SqlDbType.VarChar, 50).Value = user.BusNumber;
                            command.Parameters.Add("@PostalCode", SqlDbType.Int).Value = user.PostalCode;
                            command.Parameters.Add("@Country", SqlDbType.VarChar, 50).Value = user.Country;
                            command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = user.PhoneNumber;
                            command.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = user.EmailAddress;
                            command.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 50).Value = user.Password;
                            if (user.Active)
                            {
                                command.Parameters.Add("@Active", SqlDbType.Int, 50).Value = 1;
                            }
                            else
                            {
                                command.Parameters.Add("@Active", SqlDbType.Int, 50).Value = 0;
                            }
                            command.Parameters.Add("@Salt", SqlDbType.VarBinary, 50).Value = user.Salt;

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
            } catch (SqlException e) {
                throw e;
            }
        }
        public void DeleteUser(int id) {
            try {
                string sql = "DELETE FROM [User] WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }


        // DISH SECTION
        public List<Dish> GetAllDishes() {
            try {
                List<Dish> dishes = new List<Dish>();
                string sql = "SELECT * FROM Dish";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                Dish dish = new Dish();
                                dish.Id = (int)reader["Id"];
                                dish.Name = (string)reader["Name"];
                                dish.Description = (string)reader["Description"];
                                dish.Price = (decimal)reader["Price"];
                                dish.AvailableQuantity = (int)reader["AvailableQuantity"];
                                dishes.Add(dish);
                            }
                        }
                    }
                }
                return dishes;
            } catch (SqlException e) {
                throw e;
            }
        }

        // ORDER SECTION
        public void CreateOrder(int userId) {
            try {
                string sql = "INSERT INTO [Order] (UserId, CreationDateTime) VALUES (@UserId, @CreationDateTime)";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        command.Parameters.Add("@CreationDateTime", SqlDbType.DateTime).Value = DateTime.Now;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }
        public void AddDishToOrder(int orderId, int dishId, int quantity) {
            try {
                string sql = "INSERT INTO OrderDish (Order_Id, Dish_Id, Quantity) VALUES (@OrderId, @DishId, @Quantity)";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                        command.Parameters.Add("@DishId", SqlDbType.Int).Value = dishId;
                        command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }
        public Order GetActiveOrder(int userId) {
            try {
                string sql = "SELECT * FROM [Order] WHERE UserId = @UserId AND PaymentDateTime IS NULL";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                Order order = new Order();
                                order.Id = (int)reader["Id"];
                                order.UserId = (int)reader["UserId"];
                                order.CreatedDateTime = (DateTime)reader["CreationDateTime"];
                                return order;
                            }
                        }
                    }
                }
                return null;
            } catch (SqlException e) {
                throw e;
            }
        }
        public void PayActiveOrder(int userId) {
            try {
                string sql = "UPDATE [Order] SET PaymentDateTime = @PaymentDateTime WHERE UserId = @UserId AND PaymentDateTime IS NULL";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@PaymentDateTime", SqlDbType.DateTime).Value = DateTime.Now;
                        command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }
        public void GetAllBesteldeGerechten(int orderId) {
            try {
                string sql = "SELECT * FROM OrderDish WHERE OrderId = @OrderId";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        connection.Open();
                        command.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                OrderDish orderDish = new OrderDish();
                                orderDish.Id = (int)reader["Id"];
                                orderDish.OrderId = (int)reader["OrderId"];
                                orderDish.DishId = (int)reader["DishId"];
                                orderDish.Amount = (int)reader["Quantity"];
                            }
                        }
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }
        public List<Order> GetAllOrders() {
               try {
                string sql = "SELECT * FROM [Order]";
                List<Order> orders = new List<Order>();
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                Order order = new Order();
                                order.Id = (int)reader["Id"];
                                order.UserId = (int)reader["UserId"];
                                order.CreatedDateTime = (DateTime)reader["CreationDateTime"];
                                if (reader["PaymentDateTime"] != DBNull.Value) {
                                    order.PaymentDateTime = (DateTime)reader["PaymentDateTime"];
                                } else {
                                       order.PaymentDateTime = DateTime.MinValue;
                                }
                                orders.Add(order);
                            }
                        }
                    }
                }
                return orders;
            } catch (SqlException e) {
                throw e;
            }
        }
        public void AddOrderDish(int orderId, int dishId, int amount) {
            try {
                string sql = "INSERT INTO OrderDish (Order_Id, Dish_Id, Quantity) VALUES (@order_Id, @Dish_Id, @Quantity)";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@order_Id", SqlDbType.Int).Value = orderId;
                        command.Parameters.Add("@Dish_Id", SqlDbType.Int).Value = dishId;
                        command.Parameters.Add("@Quantity", SqlDbType.Int).Value = amount;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }
        public void UpdateOrderDish(int orderId, int dishId, int amount) {
            try {
                string sql = "UPDATE OrderDish SET Quantity = @Quantity WHERE Order_Id = @Order_Id AND Dish_Id = @Dish_Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Order_Id", SqlDbType.Int).Value = orderId;
                        command.Parameters.Add("@Dish_Id", SqlDbType.Int).Value = dishId;
                        command.Parameters.Add("@Quantity", SqlDbType.Int).Value = amount;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public void UpdateDishAmount(int id, int amount) {
            try {
                string sql = "UPDATE Dish SET AvailableQuantity = @AvailableQuantity WHERE Id=@Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@AvailableQuantity", SqlDbType.Int).Value = amount;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        // NOT USED FOR NOW

        // ADD SECTION
        public void AddDish(string name, string description, int price, int amountAvailible) {
            try {
                string sql = "INSERT INTO Dish (Name, Description, Price, AvailableQuantity) VALUES (@Name, @Description, @Price, @AvailableQuantity)";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = name;
                        command.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = description;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                        command.Parameters.Add("@AvailableQuantity", SqlDbType.Int).Value = amountAvailible;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }

        }

        public void AddOrder(int userId, DateTime createdDateTime, DateTime paymentDateTime) {
            try {
                
                string sql = "INSERT INTO [Order] (UserId, CreatedDateTime, PaymentDateTime) VALUES (@UserId, @CreatedDateTime, @PaymentDateTime)";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        command.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = createdDateTime;
                        command.Parameters.Add("@PaymentDateTime", SqlDbType.DateTime).Value = paymentDateTime;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        

        public void AddUser(string firstName, string lastName, string street, string city, int houseNumber, string busNumber, int postalCode, string country, string email, int phoneNumber, string password) {
            try {
                string sql = "INSERT INTO [User] (FirstName, LastName, Street, City, HouseNumber, BusNumber, PostalCode, Country, Email, PhoneNumber, Password) VALUES (@FirstName, @LastName, @Street, @City, @HouseNumber, @BusNumber, @PostalCode, @Country, @Email, @PhoneNumber, @Password)";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
                        command.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;
                        command.Parameters.Add("@Street", SqlDbType.VarChar, 50).Value = street;
                        command.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = city;
                        command.Parameters.Add("@HouseNumber", SqlDbType.Int).Value = houseNumber;
                        command.Parameters.Add("@BusNumber", SqlDbType.VarChar, 50).Value = busNumber;
                        if (postalCode != null) {
                            command.Parameters.Add("@PostalCode", SqlDbType.Int).Value = postalCode;
                        } else {
                            command.Parameters.Add("@PostalCode", SqlDbType.Int).Value = 0;
                        }

                        command.Parameters.Add("@Country", SqlDbType.VarChar, 50).Value = country;
                        command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = email;
                        command.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = phoneNumber;
                        command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        // DELETE SECTION

        public void DeleteDish(int id) {
            try {
                
                string sql = "DELETE FROM Dish WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        public void DeleteOrder(int id) {
            try {
                string sql = "DELETE FROM [Order] WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        public void DeleteOrderDish(int orderId, int dishId) {
            try {
                string sql = "DELETE FROM OrderDish WHERE Order_Id = @Order_Id AND Dish_Id=@Dish_Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Order_Id", SqlDbType.Int).Value = orderId;
                        command.Parameters.Add("@Dish_Id", SqlDbType.Int).Value = dishId;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }
        public void DeleteAllOrderDish(int orderId) {
            try {
                string sql = "DELETE FROM OrderDish WHERE Order_Id = @Order_Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Order_Id", SqlDbType.Int).Value = orderId;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        // GET ALL SECTION

        public List<OrderDish> GetAllOrderDishes() {
            try {
                List<OrderDish> orderDishes = new List<OrderDish>();
                string sql = "SELECT * FROM OrderDish";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                OrderDish orderDish = new OrderDish();
                                orderDish.Id = (int)reader["Id"];
                                orderDish.OrderId = (int)reader["Order_Id"];
                                orderDish.DishId = (int)reader["Dish_Id"];
                                orderDish.Amount = (int)reader["Quantity"];
                                orderDishes.Add(orderDish);
                            }
                        }
                    }
                }
                return orderDishes;
            } catch (SqlException e) {
                throw e;
            }
        }

        // GET BY ID/NAME SECTION
        public Dish GetDishById(int id) {
            try {
                string sql = "SELECT * FROM Dish WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                Dish dish = new Dish();
                                dish.Id = (int)reader["Id"];
                                dish.Name = (string)reader["Name"];
                                dish.Description = (string)reader["Description"];
                                dish.Price = (decimal)reader["Price"];
                                dish.AvailableQuantity = (int)reader["AvailableQuantity"];
                                return dish;
                            } else {
                                return null;
                            }
                        }
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        public Dish GetDishByName(string name) {
            try {
                string sql = "SELECT * FROM Dish WHERE Name = @Name";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                Dish dish = new Dish();
                                dish.Id = (int)reader["Id"];
                                dish.Name = (string)reader["Name"];
                                dish.Description = (string)reader["Description"];
                                dish.Price = (decimal)reader["Price"];
                                dish.AvailableQuantity = (int)reader["AvailableQuantity"];
                                return dish;
                            } else {
                                return null;
                            }
                        }
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        public Order GetOrderById(int id) {
            try {
                string sql = "SELECT * FROM [Order] WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                Order order = new Order();
                                order.Id = (int)reader["Id"];
                                order.UserId = (int)reader["User_Id"];
                                order.CreatedDateTime = (DateTime)reader["CreationDateTime"];
                                order.PaymentDateTime = (DateTime)reader["PaymentDateTime"];
                                return order;
                            } else {
                                return null;
                            }
                        }
                    }
                }
            } catch (SqlException e) {
                throw e;
            }
        }

        public OrderDish GetOrderDishById(int id) {
            throw new NotImplementedException();
        }

        public List<OrderDish> GetOrderDishesByDishId(int dishId) {
            throw new NotImplementedException();
        }

        public List<OrderDish> GetOrderDishesByOrderId(int orderId) {
            try {
                string sql= "SELECT * FROM OrderDish WHERE Order_Id = @OrderId";
                List<OrderDish> orderDishes = new List<OrderDish>();
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                OrderDish orderDish = new OrderDish();
                                orderDish.Id = (int)reader["Id"];
                                orderDish.OrderId = (int)reader["Order_Id"];
                                orderDish.DishId = (int)reader["Dish_Id"];
                                orderDish.Amount = (int)reader["Quantity"];
                                orderDishes.Add(orderDish);
                            }
                        }
                    }
                }
                return orderDishes;
            } catch (SqlException e) {
                throw e;
            }
        }

        public List<Order> GetOrdersByUserId(int userId) {
            throw new NotImplementedException();
        }

        // UPDATE SECTION

        public void UpdateDish(int id, string name, string description, int price, int amountAvailible) {
            try
            {
                string sql = "UPDATE Dish SET AvailableQuantity=@AvailableQuantity, Name=@Name, Description=@Discription, Price=@Price WHERE Id=@Id";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@AvailableQuantity", SqlDbType.Int).Value = amountAvailible;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                        command.Parameters.Add("@Discription", SqlDbType.NVarChar).Value = description;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateOrder(int id, int userId, DateTime createdDateTime, DateTime paymentDateTime) {
            throw new NotImplementedException();
        }



        public void UpdateOrderPaymentDateTime(int id, DateTime paymentDateTime) {
            throw new NotImplementedException();
        }

        
    }
}

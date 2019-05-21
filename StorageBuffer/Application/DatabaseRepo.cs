using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;
using StorageBuffer.Model;

namespace StorageBuffer.Application
{
    public class DatabaseRepo : IPersistable
    {
        private static DatabaseRepo instance = null;
        private static readonly object padlock = new object();

        public static DatabaseRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseRepo();
                        }
                    }
                }

                return instance;
            }
        }
        private readonly string connectionString;

        DatabaseRepo()
        {
            connectionString = "Server=EALSQL1.eal.local;Database=B_DB24_2018;User Id=B_STUDENT24;Password=B_OPENDB24;";
        }

        SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(connectionString);
        }

        public List<Customer> GetAllCustomers()
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllCustomers", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Customer> result = new List<Customer>();
                        while (reader.Read())
                        {
                            int.TryParse(reader["CustomerId"].ToString(), out int id);
                            string name = reader["CustomerName"].ToString();
                            string address = reader["CustomerAddress"].ToString();
                            int.TryParse(reader["ZIP"].ToString(), out int zip);
                            string city = reader["City"].ToString();
                            string phone = reader["Phone"].ToString();
                            string email = reader["Email"].ToString();

                            result.Add(new Customer(id, name, address, zip, city, phone, email));
                        }

                        return result;
                    }
                }
            }
        }

        public List<Material> GetAllMaterials()
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllMaterials", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Material> result = new List<Material>();
                        while (reader.Read())
                        {
                            int.TryParse(reader["MaterialId"].ToString(), out int id);
                            string name = reader["MaterialName"].ToString();
                            string comment = reader["Comment"].ToString();
                            int.TryParse(reader["Quantity"].ToString(), out int quantity);

                            result.Add(new Material(id, name, comment, quantity));
                        }

                        return result;
                    }
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllOrders", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Customer> customerRepoCustomers = CustomerRepo.Instance.customers;
                        List<Order> result = new List<Order>();
                        while (reader.Read())
                        {
                            int.TryParse(reader["OrderId"].ToString(), out int id);
                            int.TryParse(reader["CustomerId"].ToString(), out int customerId);
                            Status.TryParse(reader["OrderStatus"].ToString(), out Status status);
                            string name = reader["OrderName"].ToString();
                            string date = reader["OrderDate"].ToString();
                            string deadline = reader["Deadline"].ToString();
                            string description = reader["OrderDescription"].ToString();

                            string customerName = CustomerRepo.Instance.GetCustomerName(customerId);

                            Order order = new Order(id, customerId, customerName, status, name, date, deadline, description);
                            order.orderlines = GetOrderlinesForOrder(order);
                            result.Add(order);
                        }

                        return result;
                    }
                }
            }
        }

        private List<Orderline> GetOrderlinesForOrder(Order order)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spGetOrderlinesForOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = order.Id;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Orderline> result = new List<Orderline>();
                        while (reader.Read())
                        {
                            int.TryParse(reader["MaterialId"].ToString(), out int MaterialId);
                            int.TryParse(reader["Quantity"].ToString(), out int quantity);
                            Material material = MaterialRepo.Instance.GetMaterialObj(MaterialId);

                            result.Add(new Orderline(material, quantity));
                        }

                        return result;
                    }
                }
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = order.Id;
                    cmd.Parameters.Add("@OrderStatus", SqlDbType.NChar).Value = order.OrderStatus.ToString();
                    cmd.Parameters.Add("@OrderDescription", SqlDbType.NChar).Value = order.Description;
                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertOrderline(int orderId, int materialId, int quantity)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spInsertOrderline", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = orderId;
                    cmd.Parameters.Add("@MaterialId", SqlDbType.Int).Value = materialId;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveOrderlines(Order order)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spRemoveOrderline", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = order.Id;

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int CreateCustomer(string customerName, string customerAddress, string customerCity, int customerZip, string customerPhone, string customerEmail, string customerComment)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spInsertCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CustomerName", SqlDbType.NChar).Value = customerName;
                    cmd.Parameters.Add("@CustomerAddress", SqlDbType.NChar).Value = customerAddress;
                    cmd.Parameters.Add("@City", SqlDbType.NChar).Value = customerCity;
                    cmd.Parameters.Add("@ZIP", SqlDbType.Int).Value = customerZip;
                    cmd.Parameters.Add("@Phone", SqlDbType.NChar).Value = customerPhone;
                    cmd.Parameters.Add("@Email", SqlDbType.NChar).Value = customerEmail;
                    cmd.Parameters.Add("@CustomerComment", SqlDbType.NChar).Value = customerComment;

                    connection.Open();

                    int customerId = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerId = int.Parse(reader["CustomerId"].ToString());
                        }
                    }

                    return customerId;
                }
            }
        }

        public int CreateMaterial(string materialName, string materialComments, int quantity)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spInsertMaterial", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MaterialName", SqlDbType.NChar).Value = materialName;
                    cmd.Parameters.Add("@Comment", SqlDbType.NChar).Value = materialComments;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NChar).Value = quantity;
                    connection.Open();

                    int materialId = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materialId = int.Parse(reader["MaterialId"].ToString());
                        }
                    }

                    return materialId;
                }
            }
        }

        public int CreateOrder(int customerId, string name, string date, string deadline, string description)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spInsertOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;
                    cmd.Parameters.Add("@OrderStatus", SqlDbType.NChar).Value = Status.Received.ToString();
                    cmd.Parameters.Add("@OrderName", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@OrderDate", SqlDbType.NChar).Value = date;
                    cmd.Parameters.Add("@Deadline", SqlDbType.NChar).Value = deadline;
                    cmd.Parameters.Add("@OrderDescription", SqlDbType.NChar).Value = description;

                    connection.Open();

                    int orderId = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderId = int.Parse(reader["OrderId"].ToString());
                        }
                    }

                    return orderId;
                }
            }
        }
    }
}
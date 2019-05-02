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

namespace StorageBuffer.Application
{
    public class DatabaseRepo
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

        public DatabaseRepo()
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

        public List<Order> GetAllOrders(List<Customer> customerRepoCustomers)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllOrders", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Order> result = new List<Order>();
                        while (reader.Read())
                        {
                            int.TryParse(reader["OrderId"].ToString(), out int id);
                            int.TryParse(reader["CustomerId"].ToString(), out int customerId);
                            Status.TryParse(reader["OrderStatus"].ToString(), out Status status);
                            string name = reader["OrderName"].ToString();
                            string date = reader["OrderDate"].ToString();
                            string deadline = reader["Deadline"].ToString();

                            Customer customer = customerRepoCustomers.Find(x => x.Id == id);

                            result.Add(new Order(id, customer, status, name, date, deadline));
                        }

                        return result;
                    }
                }
            }
        }
    }
}

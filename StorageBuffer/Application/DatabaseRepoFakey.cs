using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;
using StorageBuffer.Model;

namespace StorageBuffer.Application
{
    public class DatabaseRepoFakey : IPersistable
    {
        private static DatabaseRepoFakey instance = null;
        private static readonly object padlock = new object();

        public static DatabaseRepoFakey Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseRepoFakey();
                        }
                    }
                }

                return instance;
            }
        }

        DatabaseRepoFakey()
        {
        }

        public List<Customer> GetAllCustomers()
        {
            return new List<Customer>()
            {
                new Customer(1, "Brian Mariannesen", "Odensevej 24", 5000,
                    "Odense C", "+4512345678", "brian.mariannesen@gmail.com", "ingen"),
                new Customer(2, "Torben Sørensen", "Hjallesevej 14", 5000,
                    "Odense S", "+4556905690", "torben.sørensen@gmail.com", "ingen")
            };
        }

        public List<Material> GetAllMaterials()
        {
            return new List<Material>()
            {
                new Material(11, "Bordplade i rustfri", "Ingen", 2),
                new Material(12, "6mm. Plade", "Vejer 300kg", 1)
            };
        }

        public List<Order> GetAllOrders()
        {
            List<Customer> customers = CustomerRepo.Instance.customers;
            return new List<Order>()
            {
                new Order(21, customers[0].Id, customers[0].Name, Status.Received, "Komplet Køkken", "02/02/2019", "16/02/2019", "ingen"),
                new Order(22, customers[0].Id, customers[0].Name, Status.Paid, "Hylde i Rustfri", "02/02/2019", "16/02/2019", "ingen")
            };
        }

        public bool UpdateOrder(Order order)
        {
            return true;
        }

        public bool InsertOrderline(int orderId, int materialId, int quantity)
        {
            return true;
        }

        public bool RemoveOrderlines(Order order)
        {
            return true;
        }

        private int customerId = 3;
        public int CreateCustomer(string customerName, string customerAddress, string customerCity, int customerZip, string customerPhone, string customerEmail, string customerComment)
        {
            return customerId++;
        }

        private int materialId = 13;
        public int CreateMaterial(string materialName, string materialComments, int quantity)
        {
            return materialId++;
        }

        public bool UpdateCustomer(int customerId, string customerName, string customerAddress, string customerCity, int customerZip, string customerPhone, string customerEmail, string customerComment)
        {
            return true;
        }

        public bool UpdateMaterial(int materialId, string materialName, string materialComment, int quantity)
        {
            return true;
        }

        public bool RemoveMaterial(int materialId)
        {
            return true;
        }

        public bool RemoveCustomer(int customerId)
        {
            return true;
        }

        public bool RemoveOrder(int orderId)
        {
            return true;
        }
    }
}

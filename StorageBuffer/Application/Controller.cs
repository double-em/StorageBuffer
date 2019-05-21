using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;
using StorageBuffer.Model;

namespace StorageBuffer.Application
{
    public sealed class Controller
    {
        private static Controller instance = null;
        private static readonly object padlock = new object();

        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Controller();
                        }
                    }
                }

                return instance;
            }
        }

        public CustomerRepo customerRepo;
        public MaterialRepo materialRepo;
        public OrderRepo orderRepo;

        Controller()
        {
        }

        public void GetAllData(IPersistable databaseRepo)
        {
            CustomerRepo.CreateInstance(databaseRepo);
            customerRepo = CustomerRepo.Instance;

            MaterialRepo.CreateInstance(databaseRepo);
            materialRepo = MaterialRepo.Instance;

            OrderRepo.CreateInstance(databaseRepo);
            orderRepo = OrderRepo.Instance;
        }

        public List<List<string>> FindItems(string searchCriteria, string searchQuery = "")
        {
            List<List<string>> result = new List<List<string>>();
            switch (searchCriteria)
            {
                case "Customers":
                    result.AddRange(customerRepo.GetCustomers(searchQuery));
                    break;

                case "Materials":
                    result.AddRange(materialRepo.GetMaterials(searchQuery));
                    break;

                case "Orders":
                    result.AddRange(orderRepo.GetOrders(searchQuery));
                    break;

                case "All":
                    result.AddRange(customerRepo.GetCustomers(searchQuery));
                    result.AddRange(materialRepo.GetMaterials(searchQuery));
                    result.AddRange(orderRepo.GetOrders(searchQuery));
                    break;
            }
            return result;
        }

        public void RegisterUsedMaterial(int orderId, int materialId, int amount)
        {
            orderRepo.RegisterUsedMaterial(orderId, materialId, amount);
        }

        public void ChangeStatusOfOrder(int orderId, Status status)
        {
            orderRepo.ChangeStatusOfOrder(orderId, status);
        }

        public void UpdateOrder(int orderId, string orderStatus, List<List<string>> orderlines, string description)
        {
            orderRepo.UpdateOrder(orderId, orderStatus, orderlines, description);
        }

        public bool CreateOrder(int customerId, string orderName, string deadline, string description)
        {
            return orderRepo.CreateOrder(customerId, orderName, deadline, description);
        }

        public List<string> GetOrderInfo(int orderId)
        {
            return orderRepo.GetOrderInfo(orderId);
        }

        public List<List<string>> GetOrderlines(int orderId)
        {
            return orderRepo.GetOrderlines(orderId);
        }

        public List<string> GetMaterial(int materialId)
        {
            return materialRepo.GetMaterial(materialId);
        }

        public List<string> GetMaterialLong(int materialId)
        {
            return materialRepo.GetMaterialLong(materialId);
        }

        public List<string> GetCustomer(int customerId)
        {
            return customerRepo.GetCustomer(customerId);
        }

        public void CreateCustomer(string customerName, string customerAddress, string customerCity, string customerZip, string customerPhone, string customerEmail, string customerComment)
        {
            customerRepo.CreateCustomer(customerName, customerAddress, customerCity, customerZip, customerPhone, customerEmail, customerComment);
        }

        public void CreateMaterial(string materialName, string materialComment, string materialQuantity)
        {
            materialRepo.CreateMaterial(materialName, materialComment, materialQuantity);
        }

        public void UpdateCustomer(int customerId, string customerName, string customerAddress, string customerCity, string customerZip, string customerPhone, string customerEmail, string customerComment)
        {
            int.TryParse(customerZip, out int zip);
            customerRepo.UpdateCustomer(customerId, customerName, customerAddress, customerCity, zip, customerPhone, customerEmail, customerComment);
        }

        public void UpdateMaterial(int materialId, string materialName, string materialComment, string materialQuantity)
        {
            int.TryParse(materialQuantity, out int quantity);
            materialRepo.UpdateMaterial(materialId, materialName, materialComment, quantity);
        }
    }
}

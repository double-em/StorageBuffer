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

        public void ChangeStatusOfOrder(int orderId, Status status)
        {
            orderRepo.ChangeStatusOfOrder(orderId, status);
        }

        public void UpdateOrder(int orderId, string orderStatus, List<List<string>> orderlines)
        {
            orderRepo.UpdateOrder(orderId, orderStatus, orderlines);
        }

        public bool CreateOrder(int customerId, string orderName, string deadline)
        {
            return orderRepo.CreateOrder(customerId, orderName, deadline);
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
    }
}

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

        public List<IItem> FindItems(string searchCriteria, string searchQuery = "")
        {
            List<IItem> result = new List<IItem>();
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

        public void RegisterUsedMaterial(int orderId, Material material, int amount)
        {
            orderRepo.RegisterUsedMaterial(orderId, material, amount);
        }

        public void ChangeStatusOfOrder(int orderId, Status status)
        {
            orderRepo.ChangeStatusOfOrder(orderId, status);
        }
    }
}

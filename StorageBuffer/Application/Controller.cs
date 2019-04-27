using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;
using StorageBuffer.Model;

namespace StorageBuffer.Application
{
    public class Controller
    {
        public CustomerRepo customerRepo;
        public MaterialRepo materialRepo;
        public OrderRepo orderRepo;

        public Controller()
        {
            customerRepo = new CustomerRepo();
            materialRepo = new MaterialRepo();
            orderRepo = new OrderRepo();
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
    }
}

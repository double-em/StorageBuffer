using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;

namespace StorageBuffer.Model
{
    public sealed class OrderRepo
    {
        private static OrderRepo instance = null;
        private static readonly object padlock = new object();

        public static OrderRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            throw new Exception("Object not created!");
                        }
                    }
                }
                return instance;
            }
        }

        public static void CreateInstance(IPersistable databaseRepo)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new OrderRepo(databaseRepo);
                    }
                }
            }
        }

        public List<Order> orders;

        OrderRepo(IPersistable databaseRepo)
        {
            orders = databaseRepo.GetAllOrders();
        }

        public List<IItem> GetOrders(string searchQuery)
        {
            List<IItem> result = new List<IItem>();
            foreach (Order order in orders)
            {
                if (order.Name.ToLower().Contains(searchQuery.ToLower()) || 
                    order.CustomerObj.Name.ToLower().Contains(searchQuery.ToLower()) || 
                    order.CustomerObj.Phone.Contains(searchQuery))
                {
                    result.Add(order);
                }
            }

            return result;
        }

        public void RegisterUsedMaterial(int orderId, Material material, int amount)
        {
            orders.Find(x => x.Id == orderId).RegisterUsedMaterial(material, amount);
        }

        public void ChangeStatusOfOrder(int orderId, Status status)
        {
            orders.Find(x => x.Id == orderId).OrderStatus = status;
        }

        public bool CreateOrder(Customer customer, string orderName, string orderDescription, string deadline)
        {
            throw new NotImplementedException();
        }
    }
}

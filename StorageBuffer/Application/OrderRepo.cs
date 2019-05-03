using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;

namespace StorageBuffer.Model
{
    public class OrderRepo
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
                            instance = new OrderRepo();
                        }
                    }
                }

                return instance;
            }
        }
        public List<Order> orders;

        OrderRepo()
        {
            orders = new List<Order>();
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
    }
}

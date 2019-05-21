using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Application;
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

        private IPersistable databaseRepo;

        public List<Order> orders;

        OrderRepo(IPersistable databaseRepo)
        {
            this.databaseRepo = databaseRepo;
            orders = databaseRepo.GetAllOrders();
        }

        public List<List<string>> GetOrders(string searchQuery)
        {
            List<List<string>> result = new List<List<string>>();
            foreach (Order order in orders)
            {
                if (order.Name.ToLower().Contains(searchQuery.ToLower()) || 
                    order.CustomerObj.Name.ToLower().Contains(searchQuery.ToLower()) || 
                    order.CustomerObj.Phone.Contains(searchQuery))
                {
                    result.Add(order.ToList());
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

        public bool CreateOrder(Customer customer, string orderName, string description, string deadline)
        {
            string date = DateTime.Now.ToShortDateString();
            int id = DatabaseRepo.Instance.CreateOrder(customer.Id, orderName, date, description, deadline);
            Order order = new Order(id, customer, Status.Received, orderName, date, description, deadline);
            if (order == null)
            {
                return false;
            }
            orders.Add(order);
            return true;
            
        }

        public void UpdateOrder(int orderId, string orderStatus, List<List<string>> orderlines)
        {
            Order orderResult = orders.Find(x => x.Id == orderId);

            Status.TryParse(orderStatus, out Status status);
            orderResult.OrderStatus = status;

            orderResult.orderlines = new List<Orderline>();
            databaseRepo.RemoveOrderlines(orderResult);
            databaseRepo.UpdateOrder(orderResult);

            foreach (List<string> orderline in orderlines)
            {
                //RegisterUsedMaterial(orderResult.Id, orderline[0], orderline[2]);
                //databaseRepo.InsertOrderline(orderResult.Id, orderline);
            }
        }

        public List<string> GetOrderInfo(int orderId)
        {
            return orders.Find(x => x.Id == orderId).ToLongList();
        }

        public List<List<string>> GetOrderlines(int orderId)
        {
            return orders.Find(x => x.Id == orderId).GetOrderlines();
        }
    }
}

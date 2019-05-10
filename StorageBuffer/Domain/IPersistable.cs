using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public interface IPersistable
    {
        List<Customer> GetAllCustomers();
        List<Material> GetAllMaterials();
        List<Order> GetAllOrders();
        void UpdateOrder(Order order);
        void InsertOrderline(int orderId, Orderline orderline);
        void RemoveOrderlines(Order order);
    }
}

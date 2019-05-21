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
        void InsertOrderline(int orderId, int materialId, int quantity);
        void RemoveOrderlines(Order order);
        int CreateCustomer(string customerName, string customerAddress, string customerCity, int customerZip, string customerPhone, string customerEmail, string customerComment);
        int CreateMaterial(string materialName, string materialComments, int quantity);
    }
}

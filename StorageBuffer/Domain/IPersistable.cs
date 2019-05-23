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
        bool UpdateOrder(Order order);
        bool InsertOrderline(int orderId, int materialId, int quantity);
        bool RemoveOrderlines(Order order);
        int CreateCustomer(string customerName, string customerAddress, string customerCity, int customerZip, string customerPhone, string customerEmail, string customerComment);
        int CreateMaterial(string materialName, string materialComments, int quantity);
        bool UpdateCustomer(int customerId, string customerName, string customerAddress, string customerCity, int customerZip, string customerPhone, string customerEmail, string customerComment);
        bool UpdateMaterial(int materialId, string materialName, string materialComment, int quantity);
        bool RemoveMaterial(int materialId);
        bool RemoveCustomer(int customerId);
        bool RemoveOrder(int orderId);
        int CreateOrder(int customerId, string orderName, string date, string deadline, string description);
    }
}

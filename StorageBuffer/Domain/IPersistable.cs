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
        bool UpdateOrder(int orderId, string orderStatus, string description);
        bool InsertOrderline(int orderId, int materialId, int quantity);
        bool RemoveOrderlines(int orderId);
        int CreateCustomer(string name, string address, string city, int zip, string phone, string email, string comment);
        int CreateMaterial(string name, string comment, int quantity);
        bool UpdateCustomer(int id, string name, string address, string city, int zip, string phone, string email, string comment);
        bool UpdateMaterial(int id, string name, string comment, int quantity);
        bool RemoveMaterial(int materialId);
        bool RemoveCustomer(int customerId);
        bool RemoveOrder(int orderId);
        int CreateOrder(int customerId, string name, string date, string deadline, string description);
    }
}

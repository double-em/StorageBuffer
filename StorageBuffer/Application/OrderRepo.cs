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
        public List<Order> orders;

        public OrderRepo()
        {
            orders = new List<Order>();
        }

        public void RegisterUsedMaterial(int orderId, Material material, int amount)
        {
            throw new NotImplementedException();
        }
    }
}

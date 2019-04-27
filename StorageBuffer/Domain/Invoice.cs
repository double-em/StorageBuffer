using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public List<Orderline> orderlines;

        public Invoice(int id, int customerId, int orderId, double amount)
        {
            Id = id;
            CustomerId = customerId;
            OrderId = orderId;
            Amount = amount;
            orderlines = new List<Orderline>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public enum Status
    {
        Received,
        InProgress,
        Done,
        Shipped,
        Billed,
        Paid,
        Canceled
    }
    public class Order : IItem
    {
        public int OrderId { get; set; }
        public Customer CustomerObj { get; set; }
        public Status OrderStatus { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Deadline { get; set; }

        public Order(int orderId, Customer customerObj, Status orderStatus, string name, string date, string deadline)
        {
            OrderId = orderId;
            CustomerObj = customerObj;
            OrderStatus = orderStatus;
            Name = name;
            Date = date;
            Deadline = deadline;
        }
    }
}

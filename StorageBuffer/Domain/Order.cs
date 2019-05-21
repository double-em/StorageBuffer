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
    public delegate void OrderChanged(Order order);
    public class Order : IItem
    {
        public int Id { get; set; }
        public Customer CustomerObj { get; set; }
        public Status OrderStatus { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Deadline { get; set; }
        public string Description {get; set; }
        public List<Orderline> orderlines;
        public string Type { get; } = "Order";

        public string Data
        {
            get { return $"Deadline: {Deadline}, {CustomerObj.Name}"; }
        }

        public Order(int id, Customer customerObj, Status orderStatus, string name, string date, string deadline, string Description)
        {
            Id = id;
            CustomerObj = customerObj;
            OrderStatus = orderStatus;
            Name = name;
            Date = date;
            Deadline = deadline;
            Description = description;
            orderlines = new List<Orderline>();
        }

        public void RegisterUsedMaterial(Material material, int amount)
        {
            if (material.Quantity >= amount)
            {
                Orderline orderline = orderlines.Find(x => x.MaterialObj == material);
                if (orderline != null)
                {
                    orderline.Quantity += amount;
                }
                else
                {
                    orderlines.Add(new Orderline(material, amount));
                }
                material.Quantity -= amount;
            }
        }

        public List<string> ToList()
        {
            return new List<string>() { Type, Id.ToString(), Name, Data };
        }

        public List<string> ToLongList()
        {
            return new List<string>() { Id.ToString(), Name, Date, Deadline, CustomerObj.Name, OrderStatus.ToString() };
        }

        public List<List<string>> GetOrderlines()
        {
            List<List<string>> result = new List<List<string>>();
            foreach (Orderline orderline in orderlines)
            {
                result.Add(orderline.ToList());
            }
            return result;
        }
    }
}

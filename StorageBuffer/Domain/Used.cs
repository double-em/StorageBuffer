using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Used
    {
        public Material MaterialObj { get; set; }
        public Order OrderObj { get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }

        public Used(Material materialObj, Order orderObj, int quantity, string date)
        {
            MaterialObj = materialObj;
            OrderObj = orderObj;
            Quantity = quantity;
            Date = date;
        }
    }
}

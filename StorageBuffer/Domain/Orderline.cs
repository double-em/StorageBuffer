using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Orderline
    {
        public Material MaterialObj { get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }

        public Orderline(Material materialObj, Order orderObj, int quantity, string date)
        {
            MaterialObj = materialObj;
            Quantity = quantity;
            Date = date;
        }
    }
}

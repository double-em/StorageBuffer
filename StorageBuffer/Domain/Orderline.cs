using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Orderline
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public Material MaterialObj { get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }

        public Orderline(Material materialObj, int quantity, string date)
        {
            MaterialObj = materialObj;
            MaterialId = MaterialObj.Id;
            MaterialName = MaterialObj.Name;
            Quantity = quantity;
            Date = date;
        }
    }
}

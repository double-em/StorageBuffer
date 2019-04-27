using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Material : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }

        public string Data
        {
            get { return Quantity.ToString(); }
        }

        public Material(int id, string name, string comment, int quantity)
        {
            Id = id;
            Name = name;
            Comment = comment;
            Quantity = quantity;
        }
    }
}

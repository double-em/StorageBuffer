using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Material : IItem
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }

        public Material(int materialId, string name, string comment, int quantity)
        {
            MaterialId = materialId;
            Name = name;
            Comment = comment;
            Quantity = quantity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Material
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Quantity { get; set; }

        public Material(int materialId, string name, string comment, string quantity)
        {
            MaterialId = materialId;
            Name = name;
            Comment = comment;
            Quantity = quantity;
        }
    }
}

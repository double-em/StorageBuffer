using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;

namespace StorageBuffer.Model
{
    public class MaterialRepo
    {
        public List<Material> materials;

        public MaterialRepo()
        {
            materials = new List<Material>();
        }

        public List<IItem> GetMaterials(string searchQuery)
        {
            List<IItem> result = new List<IItem>();
            foreach (Material material in materials)
            {
                if (material.Name.ToLower().Contains(searchQuery.ToLower()))
                {
                    result.Add(material);
                }
            }

            return result;
        }
    }
}

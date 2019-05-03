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
        private static MaterialRepo instance = null;
        private static readonly object padlock = new object();

        public static MaterialRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new MaterialRepo();
                        }
                    }
                }

                return instance;
            }
        }

        public List<Material> materials;

        MaterialRepo()
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

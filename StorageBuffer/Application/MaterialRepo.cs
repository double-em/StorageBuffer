using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;

namespace StorageBuffer.Model
{
    public sealed class MaterialRepo
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
                            throw new Exception("Object not created!");
                        }
                    }
                }
                return instance;
            }
        }

        public static void CreateInstance(IPersistable databaseRepo)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MaterialRepo(databaseRepo);
                    }
                }
            }
        }

        public List<Material> materials;

        MaterialRepo(IPersistable databaseRepo)
        {
            materials = databaseRepo.GetAllMaterials();
        }

        public List<List<string>> GetMaterials(string searchQuery)
        {
            List<List<string>> result = new List<List<string>>();
            foreach (Material material in materials)
            {
                if (material.Name.ToLower().Contains(searchQuery.ToLower()))
                {
                    result.Add(material.ToList());
                }
            }

            return result;
        }

        public List<string> GetMaterial(int MaterialId)
        {
            return materials.Find(x => x.Id == MaterialId).ToList();
        }

        public Material GetMaterialObj(int MaterialId)
        {
            return materials.Find(x => x.Id == MaterialId);
        }

        public List<string> GetMaterialLong(int materialId)
        {
            return materials.Find(x => x.Id == materialId).ToLongList();
        }
    }
}

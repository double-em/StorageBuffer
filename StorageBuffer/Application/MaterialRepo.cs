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
        private IPersistable databaseRepo;

        MaterialRepo(IPersistable databaseRepo)
        {
            this.databaseRepo = databaseRepo;
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

        public void CreateMaterial(string materialName, string materialComment, string materialQuantity)
        {
            int.TryParse(materialQuantity, out int quantity);
            int id = databaseRepo.CreateMaterial(materialName, materialComment, quantity);

            materials.Add(new Material(id, materialName, materialComment, quantity));
        }

        public void UpdateMaterial(int materialId, string materialName, string materialComment, int quantity)
        {
            Material material = materials.Find(x => x.Id == materialId);

            if (material != null)
            {
                databaseRepo.UpdateMaterial(materialId, materialName, materialComment, quantity);

                material.Name = materialName;
                material.Comment = materialComment;
                material.Quantity = quantity;
            }
        }
    }
}

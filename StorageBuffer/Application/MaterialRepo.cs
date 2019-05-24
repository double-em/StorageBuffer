using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
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
            PullAllMaterials();
        }

        public void PullAllMaterials()
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

        public bool CreateMaterial(string name, string comment, string materialQuantity)
        {
            int.TryParse(materialQuantity, out int quantity);
            int id = databaseRepo.CreateMaterial(name, comment, quantity);

            if (id <= 0)
            {
                return false;
            }

            materials.Add(new Material(id, name, comment, quantity));
            return true;
        }

        public bool UpdateMaterial(int id, string name, string comment, int quantity)
        {
            Material material = materials.Find(x => x.Id == id);

            if (material != null)
            {
                if(!databaseRepo.UpdateMaterial(id, name, comment, quantity)) return false;

                material.Name = name;
                material.Comment = comment;
                material.Quantity = quantity;
                return true;
            }
            return false;
        }

        public bool RemoveMaterial(int materialId)
        {
            Material material = materials.Find(x => x.Id == materialId);

            if (material == null)
            {
                return false;
            }

            if (!databaseRepo.RemoveMaterial(materialId)) return false;
            if (!materials.Remove(material)) return false;
            return true;
        }

        public bool MaterialExist(int materialId)
        {
            return materials.Exists(x => x.Id == materialId);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class CreateMaterial
    {
        private Controller control;
        private List<Customer> customers;
        private List<Material> materials;
        private List<Order> orders;

        [TestInitialize]
        public void SetupTest()
        {
            control = Controller.Instance;
            control.GetAllData(DatabaseRepoFakey.Instance);

            customers = control.customerRepo.customers;
            materials = control.materialRepo.materials;
            orders = control.orderRepo.orders;
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            control.PullAllData();
        }

        [TestMethod]
        public void CustomerAddedToList()
        {
            string name = "Spånplade 1000x1000mm";
            control.CreateMaterial(name, "Vandskadet", "1");

            Assert.IsTrue(materials.Exists(x => x.Name == name));
        }

        [TestMethod]
        public void CustomerCanBeFoundInRepo()
        {
            string name = "Vask af 1mm. Rustfast";
            control.CreateMaterial(name, "ingen", "3");
            List<string> material = control.GetMaterial(14);
            Assert.IsTrue(material[2] == name);
        }
    }
}

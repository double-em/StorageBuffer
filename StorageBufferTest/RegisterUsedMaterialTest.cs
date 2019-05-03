using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class RegisterUsedMaterialTest
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
        public void CleanupTest()
        {
            control.customerRepo = null;
            control.materialRepo = null;
            control.orderRepo = null;
        }

        [TestMethod]
        public void RegisterUsedMaterialOneItem()
        {
            control.RegisterUsedMaterial(orders[0].Id, materials[1], 1);

            Assert.AreEqual(1, orders[0].orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialMultipleItems()
        {
            control.RegisterUsedMaterial(orders[0].Id, materials[1], 1);
            control.RegisterUsedMaterial(orders[0].Id, materials[0], 1);

            Assert.AreEqual(2, orders[0].orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialTooManyItems()
        {
            control.RegisterUsedMaterial(orders[0].Id, materials[1], 2);

            Assert.AreEqual(2, orders[0].orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialSameItem()
        {
            control.RegisterUsedMaterial(orders[0].Id, materials[0], 1);
            control.RegisterUsedMaterial(orders[0].Id, materials[0], 1);

            Assert.AreEqual(1, orders[0].orderlines[0].Quantity);
            Assert.AreEqual(2, orders[0].orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialQuantityLowered()
        {
            control.RegisterUsedMaterial(orders[0].Id, materials[0], 1);

            Assert.AreEqual(0, materials[0].Quantity);
        }
    }
}

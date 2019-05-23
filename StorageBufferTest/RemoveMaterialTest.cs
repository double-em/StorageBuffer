using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class RemoveMaterialTest
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

        [TestMethod]
        public void RemoveMaterialReturnsTrueOnSuccess()
        {
            bool succeeded = control.RemoveMaterial(11);

            Assert.IsTrue(succeeded);
        }

        [TestMethod]
        public void RemoveMaterial_MaterialRemoved()
        {
            control.RemoveMaterial(12);

            Assert.IsFalse(control.MaterialExist(12));
        }

        [TestMethod]
        public void RemoveMaterial_MaterialDosentExists()
        {
            bool succeeded = control.RemoveMaterial(14);

            Assert.IsFalse(succeeded);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class RemoveOrderTest
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
        public void RemoveOrderReturnsTrueOnSuccess()
        {
            bool succeeded = control.RemoveOrder(21);

            Assert.IsTrue(succeeded);
        }

        [TestMethod]
        public void RemoveOrder_OrderRemoved()
        {
            control.RemoveOrder(22);

            Assert.IsFalse(control.OrderExists(22));
        }

        [TestMethod]
        public void RemoveOrder_OrderDosentExists()
        {
            bool succeeded = control.RemoveOrder(23);

            Assert.IsFalse(succeeded);
        }
    }
}

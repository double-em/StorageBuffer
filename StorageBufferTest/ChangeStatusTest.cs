using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class ChangeStatusTest
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
        public void ChangeStateOfOrder()
        {
            control.ChangeStatusOfOrder(orders[0].Id, Status.Shipped);

            Assert.AreEqual(Status.Shipped, orders[0].OrderStatus);
        }
    }
}

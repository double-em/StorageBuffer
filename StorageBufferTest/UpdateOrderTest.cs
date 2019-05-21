using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class UpdateOrderTest
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
        public void UpdateOrderDescription()
        {
            control.UpdateOrder(22, "Paid", control.GetOrderlines(22), "Test 1");
            List<string> order = control.GetOrderInfo(22);
            Assert.AreEqual("Test 1", order[6]);
        }

        [TestMethod]
        public void UpdateOrderStatus()
        {
            control.UpdateOrder(22, "Billed", control.GetOrderlines(22), "Test 1");
            List<string> order = control.GetOrderInfo(22);
            Assert.AreEqual("Billed", order[5]);
        }

        [TestMethod]
        public void UpdateOrderOrderlines()
        {
            List<List<string>> orderlines = control.GetOrderlines(22);
            orderlines.Add(new List<string>(){ "11", "Bordplade i rustfri", "1"});
            control.UpdateOrder(22, "Paid", orderlines, "ingen");

            int i = 0;
            foreach (List<string> orderline in control.GetOrderlines(22))
            {
                Assert.AreEqual(orderlines[i][0], orderline[0]);
                i++;
            }
        }
    }
}

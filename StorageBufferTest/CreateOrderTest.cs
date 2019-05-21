using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class CreateOrderTest
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
        public void OrderAddedToOrderList()
        {
            string orderName = "Bordplade med skarpkantet vask";
            control.CreateOrder(customers[0].Id, orderName, "02/02/2019", "ingen");

            Assert.IsTrue(orders.Exists(x => x.Name == orderName));
        }

        [TestMethod]
        public void OrderCreationReturnsTrueIfAdded()
        {
            bool result = control.CreateOrder(customers[0].Id, "Bordplade med skarpkantet vask", "02/02/2019", "ingen");

            Assert.IsTrue(result);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class ChangeStatusTest
    {
        private Controller control;

        private Customer customer1;

        private Order order1;

        [TestInitialize]
        public void SetupTest()
        {
            control = Controller.Instance;

            customer1 = new Customer(1, "Brian Mariannesen", "Odensevej 24", 5000,
                "Odense C", "+4512345678", "brian.mariannesen@gmail.com");

            order1 = new Order(21, customer1, Status.Received, "Komplet Køkken", "02/02/2019", "16/02/2019");
            control.orderRepo.orders.Add(order1);
        }

        [TestMethod]
        public void ChangeStateOfOrder()
        {
            control.ChangeStatusOfOrder(order1.Id, Status.Shipped);

            Assert.AreEqual(Status.Shipped, order1.OrderStatus);
        }
    }
}

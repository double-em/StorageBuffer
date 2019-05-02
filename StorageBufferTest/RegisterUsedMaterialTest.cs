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

        private Customer customer1;

        private Material material1;
        private Material material2;

        private Order order1;

        [TestInitialize]
        public void SetupTest()
        {
            control = Controller.Instance;

            customer1 = new Customer(1, "Brian Mariannesen", "Odensevej 24", 5000,
                "Odense C", "+4512345678", "brian.mariannesen@gmail.com");

            material1 = new Material(11, "Bordplade i rustfri", "Ingen", 4);
            material2 = new Material(12, "6mm. Plade", "Vejer 300kg", 1);

            order1 = new Order(21, customer1, Status.Received, "Komplet Køkken", "02/02/2019", "16/02/2019");
            control.orderRepo.orders.Add(order1);
        }

        [TestMethod]
        public void RegisterUsedMaterialOneItem()
        {
            control.RegisterUsedMaterial(order1.Id, material2, 1);

            Assert.AreEqual(1, order1.orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialMultipleItems()
        {
            control.RegisterUsedMaterial(order1.Id, material2, 1);
            control.RegisterUsedMaterial(order1.Id, material1, 1);

            Assert.AreEqual(2, order1.orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialTooManyItems()
        {
            control.RegisterUsedMaterial(order1.Id, material2, 2);

            Assert.AreEqual(0, order1.orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialSameItem()
        {
            control.RegisterUsedMaterial(order1.Id, material1, 1);
            control.RegisterUsedMaterial(order1.Id, material1, 1);

            Assert.AreEqual(2, order1.orderlines[0].Quantity);
            Assert.AreEqual(1, order1.orderlines.Count);
        }

        [TestMethod]
        public void RegisterUsedMaterialQuantityLowered()
        {
            control.RegisterUsedMaterial(order1.Id, material1, 1);

            Assert.AreEqual(3, material1.Quantity);
        }
    }
}

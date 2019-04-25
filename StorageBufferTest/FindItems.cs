using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;
using StorageBuffer.Model;

namespace StorageBufferTest
{
    [TestClass]
    public class FindItems
    {
        [TestInitialize]
        public void SetupTest()
        {
            Controller control = new Controller();

            List<Customer> customers = control.customerRepo.customers;
            List<Material> materials = control.materialRepo.materials;
            List<Order> orders = control.orderRepo.orders;


        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}

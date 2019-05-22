using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class UpdateCustomer
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
        public void CustomerPhoneChange()
        {
            string phone = "+4588888888";
            control.UpdateCustomer(1, "Brian Mariannesen", "Odensevej 24", "5000",
                "Odense C", phone, "brian.mariannesen@gmail.com", "ingen");

            List<string> customer = control.GetCustomer(1);

            Assert.IsTrue(customer[5] == phone);
        }
    }
}

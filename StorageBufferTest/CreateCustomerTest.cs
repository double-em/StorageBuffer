using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class CreateCustomerTest
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
        public void CustomerAddedToList()
        {
            string name = "Brian Torsen";
            control.CreateCustomer(name, "Havnegade 3", "Odense C", "5000", "+4538475392", "email@email.com", "ingen");

            Assert.IsTrue(customers.Exists(x => x.Name == name));
        }

        [TestMethod]
        public void CustomerCanBeFoundInRepo()
        {
            string name = "Carsten Torsen";
            control.CreateCustomer(name, "Havnegade 3", "Odense C", "5000", "+4538475392", "email@email.com", "ingen");
            List<string> customer = control.GetCustomer(4);
            Assert.IsTrue(customer[1] == name);
        }
    }
}

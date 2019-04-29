using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;
using StorageBuffer.Model;

namespace StorageBufferTest
{
    [TestClass]
    public class FindItemsTest
    {
        private Controller control;
        private List<Customer> customers;
        private List<Material> materials;
        private List<Order> orders;

        private Customer customer1;
        private Customer customer2;

        [TestInitialize]
        public void SetupTest()
        {
            control = new Controller();

            customers = control.customerRepo.customers;
            materials = control.materialRepo.materials;
            orders = control.orderRepo.orders;

            customer1 = new Customer(1, "Brian Mariannesen", "Odensevej 24", 5000, 
                "Odense C", "+4512345678", "brian.mariannesen@gmail.com");

            customer2 = new Customer(2, "Torben Sørensen", "Hjallesevej 14", 5000, 
                "Odense S", "+4556905690", "torben.sørensen@gmail.com");

            customers.Add(customer1);
            customers.Add(customer2);

            materials.Add(new Material(11, "Bordplade i rustfri", "Ingen", 2));
            materials.Add(new Material(12, "6mm. Plade", "Vejer 300kg", 1));

            orders.Add(new Order(21, customer1.Id, Status.Received, "Komplet Køkken", "02/02/2019", "16/02/2019"));
            orders.Add(new Order(22, customer1.Id, Status.Paid, "Hylde i Rustfri", "02/02/2019", "16/02/2019"));
        }



        [TestMethod]
        public void GetAllCustomers()
        {
            List<IItem> result = control.FindItems("Customers");
            int I = 0;
            foreach (Customer customer in customers)
            {
                Assert.AreEqual(customer, result[I]);
                I++;
            }
        }

        [TestMethod]
        public void GetCustomerByName()
        {
            List<IItem> result = control.FindItems("Customers", "Brian Mariannesen");

            Assert.AreEqual(customers[0], result[0]);
        }

        [TestMethod]
        public void GetCustomerByPartialName()
        {
            List<IItem> result = control.FindItems("Customers", "rian Mar");

            Assert.AreEqual(customers[0], result[0]);
        }

        [TestMethod]
        public void GetCustomerByNameLowercaseAndUppercaseMixed()
        {
            List<IItem> result = control.FindItems("Customers", "bRiAn mArianNesen");

            Assert.AreEqual(customers[0], result[0]);
        }

        [TestMethod]
        public void GetCustomerByPhoneNumber()
        {
            List<IItem> result = control.FindItems("Customers", "+4512345678");

            Assert.AreEqual(customers[0].Id, result[0].Id);
        }

        [TestMethod]
        public void GetCustomerByPartialPhoneNumber()
        {
            List<IItem> result = control.FindItems("Customers", "3456");

            Assert.AreEqual(customers[0].Id, result[0].Id);
        }



        [TestMethod]
        public void GetAllMaterials()
        {
            List<IItem> result = control.FindItems("Materials");

            Assert.AreEqual(materials, result);
        }

        [TestMethod]
        public void GetMaterialByName()
        {
            List<IItem> result = control.FindItems("Materials", "6mm. Plade");

            Assert.AreEqual(materials[1], result[1]);
        }

        [TestMethod]
        public void GetMaterialByPartialName()
        {
            List<IItem> result = control.FindItems("Materials", "Plade");

            Assert.AreEqual(materials[1], result[1]);
        }

        [TestMethod]
        public void GetMaterialByNameLowercaseAndUppercaseMixed()
        {
            List<IItem> result = control.FindItems("Materials", "6MM. plAde");

            Assert.AreEqual(materials[1], result[1]);
        }



        [TestMethod]
        public void GetAllOrders()
        {
            List<IItem> result = control.FindItems("Orders");

            Assert.AreEqual(orders, result);
        }

        [TestMethod]
        public void GetOrdersByCustomerName()
        {
            List<IItem> result = control.FindItems("Orders", "Brian Mariannesen");

            Assert.AreEqual(orders, result);
        }

        [TestMethod]
        public void GetOrdersByCustomerPartialName()
        {
            List<IItem> result = control.FindItems("Customers", "rian Mar");

            Assert.AreEqual(orders, result);
        }

        [TestMethod]
        public void GetOrdersByCustomerNameLowercaseAndUppercaseMixed()
        {
            List<IItem> result = control.FindItems("Customers", "bRiAn mArianNesen");

            Assert.AreEqual(orders, result);
        }

        [TestMethod]
        public void GetOrdersByCustomerPhone()
        {
            List<IItem> result = control.FindItems("Orders", "+4512345678");

            Assert.AreEqual(orders, result);
        }

        [TestMethod]
        public void GetOrdersByCustomerPartialPhoneNumber()
        {
            List<IItem> result = control.FindItems("Customers", "3456");

            Assert.AreEqual(orders, result);
        }

        [TestMethod]
        public void GetAllItems()
        {
            List<IItem> result = control.FindItems("All");

            Assert.AreEqual(6, result.Count);
        }
    }
}

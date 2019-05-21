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
        public void GetAllCustomers()
        {
            List<List<string>> result = control.FindItems("Customers");
            int i = 0;
            foreach (Customer customer in customers)
            {
                Assert.AreEqual(customers[0].Id.ToString(), result[0][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetCustomerByName()
        {
            List<List<string>> result = control.FindItems("Customers", "Brian Mariannesen");

            Assert.AreEqual(customers[0].Id.ToString(), result[0][1]);
        }

        [TestMethod]
        public void GetCustomerByPartialName()
        {
            List<List<string>> result = control.FindItems("Customers", "rian Mar");

            Assert.AreEqual(customers[0].Id.ToString(), result[0][1]);
        }

        [TestMethod]
        public void GetCustomerByNameLowercaseAndUppercaseMixed()
        {
            List<List<string>> result = control.FindItems("Customers", "bRiAn mArianNesen");

            Assert.AreEqual(customers[0].Id.ToString(), result[0][1]);
        }

        [TestMethod]
        public void GetCustomerByPhoneNumber()
        {
            List<List<string>> result = control.FindItems("Customers", "+4512345678");

            Assert.AreEqual(customers[0].Id.ToString(), result[0][1]);
        }

        [TestMethod]
        public void GetCustomerByPartialPhoneNumber()
        {
            List<List<string>> result = control.FindItems("Customers", "3456");

            Assert.AreEqual(customers[0].Id.ToString(), result[0][1]);
        }



        [TestMethod]
        public void GetAllMaterials()
        {
            List<List<string>> result = control.FindItems("Materials");

            int i = 0;
            foreach (Material material in materials)
            {
                Assert.AreEqual(material.Id.ToString(), result[i][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetMaterialByName()
        {
            List<List<string>> result = control.FindItems("Materials", "6mm. Plade");

            Assert.AreEqual(materials[1].Id.ToString(), result[0][1]);
        }

        [TestMethod]
        public void GetMaterialByPartialName()
        {
            List<List<string>> result = control.FindItems("Materials", "Plade");

            Assert.AreEqual(materials[1].Id.ToString(), result[1][1]);
        }

        [TestMethod]
        public void GetMaterialByNameLowercaseAndUppercaseMixed()
        {
            List<List<string>> result = control.FindItems("Materials", "6MM. plAde");

            Assert.AreEqual(materials[1].Id.ToString(), result[0][1]);
        }



        [TestMethod]
        public void GetAllOrders()
        {
            List<List<string>> result = control.FindItems("Orders");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order.Id.ToString(), result[i][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByOrderName()
        {
            List<List<string>> result = control.FindItems("Orders", "Komplet Køkken");

            Assert.AreEqual(orders[0].Id.ToString(), result[0][1]);
        }

        [TestMethod]
        public void GetOrdersByOrderNamePartialAndMixed()
        {
            List<List<string>> result = control.FindItems("Orders", "hYlde");

            Assert.AreEqual(orders[1].Id.ToString(), result[0][1]);
        }

        public void GetOrdersByCustomerName()
        {
            List<List<string>> result = control.FindItems("Orders", "Brian Mariannesen");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order.Id.ToString(), result[i][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerPartialName()
        {
            List<List<string>> result = control.FindItems("Orders", "rian Mar");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order.Id.ToString(), result[i][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerNameLowercaseAndUppercaseMixed()
        {
            List<List<string>> result = control.FindItems("Orders", "bRiAn mArianNesen");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order.Id.ToString(), result[i][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerPhone()
        {
            List<List<string>> result = control.FindItems("Orders", "+4512345678");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order.Id.ToString(), result[i][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerPartialPhoneNumber()
        {
            List<List<string>> result = control.FindItems("Orders", "3456");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order.Id.ToString(), result[i][1]);
                i++;
            }
        }

        [TestMethod]
        public void GetAllItems()
        {
            List<List<string>> result = control.FindItems("All");

            Assert.AreEqual(6, result.Count);
        }
    }
}

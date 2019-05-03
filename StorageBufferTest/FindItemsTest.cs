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

            int i = 0;
            foreach (Material material in materials)
            {
                Assert.AreEqual(material, result[i]);
                i++;
            }
        }

        [TestMethod]
        public void GetMaterialByName()
        {
            List<IItem> result = control.FindItems("Materials", "6mm. Plade");

            Assert.AreEqual(materials[1].Id, result[0].Id);
        }

        [TestMethod]
        public void GetMaterialByPartialName()
        {
            List<IItem> result = control.FindItems("Materials", "Plade");

            Assert.AreEqual(materials[1].Id, result[1].Id);
        }

        [TestMethod]
        public void GetMaterialByNameLowercaseAndUppercaseMixed()
        {
            List<IItem> result = control.FindItems("Materials", "6MM. plAde");

            Assert.AreEqual(materials[1].Id, result[0].Id);
        }



        [TestMethod]
        public void GetAllOrders()
        {
            List<IItem> result = control.FindItems("Orders");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order, result[i]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByOrderName()
        {
            List<IItem> result = control.FindItems("Orders", "Komplet Køkken");

            Assert.AreEqual(orders[0].Id, result[0].Id);
        }

        [TestMethod]
        public void GetOrdersByOrderNamePartialAndMixed()
        {
            List<IItem> result = control.FindItems("Orders", "hYlde");

            Assert.AreEqual(orders[1].Id, result[0].Id);
        }

        public void GetOrdersByCustomerName()
        {
            List<IItem> result = control.FindItems("Orders", "Brian Mariannesen");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order, result[i]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerPartialName()
        {
            List<IItem> result = control.FindItems("Orders", "rian Mar");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order, result[i]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerNameLowercaseAndUppercaseMixed()
        {
            List<IItem> result = control.FindItems("Orders", "bRiAn mArianNesen");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order, result[i]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerPhone()
        {
            List<IItem> result = control.FindItems("Orders", "+4512345678");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order, result[i]);
                i++;
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerPartialPhoneNumber()
        {
            List<IItem> result = control.FindItems("Orders", "3456");

            int i = 0;
            foreach (Order order in orders)
            {
                Assert.AreEqual(order, result[i]);
                i++;
            }
        }

        [TestMethod]
        public void GetAllItems()
        {
            List<IItem> result = control.FindItems("All");

            Assert.AreEqual(6, result.Count);
        }
    }
}

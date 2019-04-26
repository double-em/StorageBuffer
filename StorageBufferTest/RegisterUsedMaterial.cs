using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBufferTest
{
    [TestClass]
    public class RegisterUsedMaterial
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

            orders.Add(new Order(21, customer1, Status.Received, "Komplet Køkken", "02/02/2019", "16/02/2019"));
            orders.Add(new Order(22, customer1, Status.Paid, "Hylde i Rustfri", "02/02/2019", "16/02/2019"));
        }

        [TestMethod]
        public void RegisterUsedMaterialStandard()
        {
            control.RegisterUsedMaterial(21, )
        }
    }
}

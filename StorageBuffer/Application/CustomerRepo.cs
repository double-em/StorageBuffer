using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;

namespace StorageBuffer.Model
{
    public sealed class CustomerRepo
    {
        private static CustomerRepo instance = null;
        private static readonly object padlock = new object();

        public static CustomerRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            throw new Exception("Object not created!");
                        }
                    }
                }
                return instance;
            }
        }

        public static void CreateInstance(IPersistable databaseRepo)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerRepo(databaseRepo);
                    }
                }
            }
        }

        public List<Customer> customers;
        private IPersistable databaseRepo;

        CustomerRepo(IPersistable databaseRepo)
        {
            this.databaseRepo = databaseRepo;
            customers = databaseRepo.GetAllCustomers();
        }

        public List<List<string>> GetCustomers(string searchQuery)
        {
            List<List<string>> result = new List<List<string>>();
            foreach (Customer customer in customers)
            {
                if (customer.Name.ToLower().Contains(searchQuery.ToLower()) || 
                    customer.Phone.Contains(searchQuery))
                {
                    result.Add(customer.ToList());
                }
            }

            return result;
        }

        public string GetCustomerName(int customerId)
        {
            return customers.Find(x => x.Id == customerId).Name;
        }

        public string GetCustomerPhone(int customerId)
        {
            return customers.Find(x => x.Id == customerId).Phone;
        }

        public List<string> GetCustomer(int customerId)
        {
            return customers.Find(x => x.Id == customerId).ToLongList();
        }

        public bool CreateCustomer(string customerName, string customerAddress, string customerCity, string customerZip, string customerPhone, string customerEmail, string customerComment)
        {
            int.TryParse(customerZip, out int zip);
            int id = databaseRepo.CreateCustomer(customerName, customerAddress, customerCity, zip, customerPhone, customerEmail, customerComment);

            if (id <= 0)
            {
                return false;
            }

            customers.Add(new Customer(id, customerName, customerAddress, zip, customerCity, customerPhone, customerEmail, customerComment));
            return true;
        }

        public bool UpdateCustomer(int customerId, string customerName, string customerAddress, string customerCity, int customerZip, string customerPhone, string customerEmail, string customerComment)
        {

            Customer customer = customers.Find(x => x.Id == customerId);

            if (customer != null)
            {
                if(!databaseRepo.UpdateCustomer(customerId, customerName, customerAddress, customerCity, customerZip, customerPhone, customerEmail, customerComment)) return false;

                customer.Name = customerName;
                customer.Address = customerAddress;
                customer.City = customerCity;
                customer.Zip = customerZip;
                customer.Phone = customerPhone;
                customer.Email = customerEmail;
                customer.Comment = customerComment;
                return true;
            }

            return false;
        }

        public bool RemoveCustomer(int customerId)
        {
            Customer customer = customers.Find(x => x.Id == customerId);

            if (customer == null)
            {
                return false;
            }

            if (!databaseRepo.RemoveCustomer(customerId)) return false;
            if (!customers.Remove(customer)) return false;
            return true;
        }

        public bool CustomerExist(int customerId)
        {
            return customers.Find(x => x.Id == customerId) != null;
        }
    }
}

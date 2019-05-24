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
            PullAllCustomers();
        }

        public void PullAllCustomers()
        {
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

        public bool CreateCustomer(string name, string address, string city, string zip, string phone, string email, string comment)
        {
            int.TryParse(zip, out int zipConvert);
            int id = databaseRepo.CreateCustomer(name, address, city, zipConvert, phone, email, comment);

            if (id <= 0)
            {
                return false;
            }

            customers.Add(new Customer(id, name, address, zipConvert, city, phone, email, comment));
            return true;
        }

        public bool UpdateCustomer(int id, string name, string address, string city, int zip, string phone, string email, string comment)
        {

            Customer customer = customers.Find(x => x.Id == id);

            if (customer != null)
            {
                if(!databaseRepo.UpdateCustomer(id, name, address, city, zip, phone, email, comment)) return false;

                customer.Name = name;
                customer.Address = address;
                customer.City = city;
                customer.Zip = zip;
                customer.Phone = phone;
                customer.Email = email;
                customer.Comment = comment;
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
            return customers.Exists(x => x.Id == customerId);
        }
    }
}

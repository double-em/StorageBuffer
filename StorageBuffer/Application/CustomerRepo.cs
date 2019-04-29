using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBuffer.Domain;

namespace StorageBuffer.Model
{
    public class CustomerRepo
    {
        public List<Customer> customers;

        public CustomerRepo()
        {
            customers = new List<Customer>();
        }

        public List<IItem> GetCustomers(string searchQuery)
        {
            if (searchQuery != "Customers"){
                List<IItem> result = customers.Where(customer => customer.Name.Contains(searchQuery)).ToList();

            }
            // If searchQuery is Customers, return all customers
            return customers;
        }
    }
}

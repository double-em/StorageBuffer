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
            List<IItem> result = new List<IItem>();
                foreach (Customer customer in customers)
                {
                    if (customer.Name.ToLower().Contains(searchQuery.ToLower()) || customer.Phone.Contains(searchQuery))
                    {
                        result.Add(customer);
                    }
                }

                return result;
        }
    }
}

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
        private List<Customer> customers;

        public CustomerRepo()
        {
            customers = new List<Customer>();
        }
    }
}

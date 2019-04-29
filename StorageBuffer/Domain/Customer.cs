using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Domain
{
    public class Customer : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Type { get; } = "Customer";

        public string Data
        {
            get { return $"{Address}, {City} {Zip}"; }
        }

        public Customer(int id, string name, string address, int zip, string city, string phone, string email)
        {
            Id = id;
            Name = name;
            Address = address;
            Zip = zip;
            City = city;
            Phone = phone;
            Email = email;
        }
    }
}

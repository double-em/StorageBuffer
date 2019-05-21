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
        public string Comment { get; set; }
        public string Type { get; } = "Customer";

        public string Data
        {
            get { return $"{Address}, {City} {Zip}"; }
        }

        public Customer(int id, string name, string address, int zip, string city, string phone, string email, string comment)
        {
            Id = id;
            Name = name;
            Address = address;
            Zip = zip;
            City = city;
            Phone = phone;
            Email = email;
            Comment = comment;
        }

        public List<string> ToList()
        {
            return new List<string>() {Type, Id.ToString(), Name, Data};
        }
        public List<string> ToLongList()
        {
            return new List<string>() { Id.ToString(), Name, Address, City, Zip.ToString(), Phone, Email, Comment };
        }
    }
}

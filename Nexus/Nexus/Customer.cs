using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class Customer
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }

        public Customer()
        {
            Id = 0;
            Name = "";
            Phone = "";
            Email = "";
            Address = "";
            City = "";
            State = "";
            Zip = "";
        }

        public void InsertCustomer()
        {
            string query = "INSERT INTO Customer (Name, Email, Phone, Addr, City, State, Zip) VALUES ('" +
                Name + "', '" + Email + "', '" + Phone + "', '" + Address + "', '" + City + "', '" + State +
                "', '" + Zip + "')";

            DBHandler.ExecuteNoReturn(query);
        }
    }
}

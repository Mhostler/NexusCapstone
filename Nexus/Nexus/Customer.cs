using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// Class representing customer information
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }

        /// <summary>
        /// default constructor fills in emtpy values
        /// </summary>
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

        /// <summary>
        /// Sends an update query to the database to save values
        /// </summary>
        public void UpdateCustomer()
        {
            string query = "UPDATE Customer SET Name='" + Name + "', Email='" + Email + "', Phone='" + Phone + "', Addr='" + Address +
                "', City='" + City + "', State='" + State + "', Zip='" + Zip + "' WHERE CustID=" + Id.ToString();

            DBHandler.ExecuteNoReturn(query);
        }

        /// <summary>
        /// Inserts the customer into the database
        /// </summary>
        public void InsertCustomer()
        {
            string query = "INSERT INTO Customer (Name, Email, Phone, Addr, City, State, Zip) VALUES ('" +
                Name + "', '" + Email + "', '" + Phone + "', '" + Address + "', '" + City + "', '" + State +
                "', '" + Zip + "')";

            DBHandler.ExecuteNoReturn(query);
        }
    }
}

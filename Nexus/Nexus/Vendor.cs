using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// Representation of a vendor and they're information
    /// </summary>
    class Vendor
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Addr { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String International { get; set; }
        public List<VendorItem> catalogue;

        /// <summary>
        /// default constructor sets values to zero
        /// </summary>
        public Vendor()
        {
            Id = 0;
            Name = "Empty";
            Email = "";
            Phone = "";
            Addr = "";
            City = "";
            State = "";
            Zip = "";
            International = "";
            catalogue = new List<VendorItem>();
        }

        /// <summary>
        /// inserts vendor to database
        /// </summary>
        public void InsertVendor()
        {
            String query = "INSERT INTO Vendor (Name, Email, Phone, Addr, City, State, Zip, International) VALUES(" +
                  '\'' + Name + "', '" + Email + "', '" + Phone + "', '" + Addr + "', '" + City + "', '" + State +
                 "', '" + Zip + "', '" + International + "')";

            DBHandler.ExecuteNoReturn(query);
        }

        /// <summary>
        /// updates vendor in database
        /// </summary>
        public void UpdateVendor()
        {
            string query = "UPDATE Vendor SET Name='" + Name + "', Email='" + Email + "', Phone='" + Phone + "', Addr='" + Addr +
                "', City='" + City + "', State=" + State + "', Zip='" + Zip + "', International='" + International + "' WHERE VendorID=" + Id.ToString();

            DBHandler.ExecuteNoReturn(query);
        }

        /// <summary>
        /// gets the list of items for the vendor
        /// </summary>
        public void getCatalogue()
        {
            catalogue = DBHandler.SelectVendorItems(this);
        }
    }
}

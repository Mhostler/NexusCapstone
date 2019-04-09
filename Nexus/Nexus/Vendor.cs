using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
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

        public Vendor()
        {
            catalogue = new List<VendorItem>();
        }

        public void InsertVendor()
        {
            String query = "INSERT INTO Vendor (Name, Email, Phone, Addr, City, State, Zip, International) VALUES(" +
                  '\'' + Name + "', '" + Email + "', '" + Phone + "', '" + Addr + "', '" + City + "', '" + State +
                 "', '" + Zip + "', '" + International + "')";

            DBHandler.ExecuteNoReturn(query);
        }

        public void UpdateVendor()
        {
            string query = "UPDATE Vendor SET Name='" + Name + "', Email='" + Email + "', Phone='" + Phone + "', Addr='" + Addr +
                "', City='" + City + "', State=" + State + "', Zip='" + Zip + "', International='" + International + "' WHERE VendID=" + Id.ToString();
        }

        public void getCatalogue()
        {
            catalogue = DBHandler.SelectVendorItems(this);
        }
    }
}

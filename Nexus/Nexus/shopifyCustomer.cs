using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class shopifyCustomer
    {
        public class Address
        {
            public Address()
            {
                this.address1 = " ";
                this.address2 = " ";
                this.city = " ";
                this.province = " ";
                this.country = " ";
                this.zip = " ";
                this.phone = " ";
            }

            public Address(object id, object customer_id, string first_name, string last_name, string addres1, string address2, string city, string province, 
                                    string country, string zip, string phone)
            {
                this.id = id;
                this.customer_id = customer_id;
                this.first_name = first_name;
                this.last_name = last_name;
                this.address1 = address1;
                this.address2 = address2;
                this.city = city;
                this.province = province;
                this.country = country;
                this.zip = zip;
                this.phone = phone;

            }
            public object id { get; set; }
            public object customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string company { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string zip { get; set; }
            public string phone { get; set; }
            public string name { get; set; }
            public string province_code { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public Boolean def { get; set; }
        }

        public class Default_Add
        {
            public object id { get; set; }
            public object customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string company { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string zip { get; set; }
            public string phone { get; set; }
            public string name { get; set; }
            public string province_code { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public Boolean def { get; set; }
        }

        public class Customers
        {
            public object id { get; set; }
            public string email { get; set; }
            public Boolean acceptes_marketing { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public int orders_count { get; set; }
            public string state { get; set; }
            public string total_spent { get; set; }
            public object last_order_id { get; set; }
            public string note { get; set; }
            public Boolean verified_email { get; set; }
            public object multipass_identifier { get; set; }
            public Boolean tax_exempt { get; set; }
            public string phone { get; set; }
            public string tags { get; set; }
            public string last_order_name { get; set; }
            public string currency { get; set; }
            public List<Address> address { get; set; }
            public string accepts_marketing_update_at { get; set; }
            public string marketing_opt_in_level { get; set; }
            public string admin_graphql_api_id { get; set; }
            public Default_Add default_address { get; set; }

            public Customer GetCust()
            {
                Customer cust = new Customer();
                cust = DBHandler.getCustomerByEmail(email);
                string name = first_name + " " + last_name;
                /*Console.WriteLine("Name:" + name);
                Console.WriteLine("Address:" + address[0].address1);
                Console.WriteLine("City:" + address[0].city);
                Console.WriteLine("State:" + address[0].province);
                Console.WriteLine("Zip:" + address[0].zip);
                Console.WriteLine("Phone:" + address[0].phone);*/
                if ( cust.Id > 0)
                {
                    cust.Name = name;
                    cust.City = default_address.city;
                    cust.Address = default_address.address1;
                    cust.Phone = default_address.phone;
                    cust.State = default_address.province;
                    cust.Zip = default_address.zip;
                    cust.UpdateCustomer();
                } else
                {
                    cust.Name = name;
                    cust.City = default_address.city;
                    cust.Address = default_address.address1;
                    cust.Phone = default_address.phone;
                    cust.State = default_address.province;
                    cust.Zip = default_address.zip;
                    cust.InsertCustomer();
                }
                return cust;
            }
        }

        public class RootObject1
        {
            public List<Customers> customers { get; set; }

            public Customer[] GetCustomers()
            {
                Customer[] cust = new Customer[customers.Count];
                for (int i = 0; i < customers.Count; i++)
                {
                    cust[i] = this.customers[i].GetCust();
                }
                return cust;
            }
        }
    }
}

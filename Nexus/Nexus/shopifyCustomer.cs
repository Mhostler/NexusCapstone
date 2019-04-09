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

            /*public Customer GetValues()
            {
                Customer cust = new Customer();
                cust = DBHandler.getCustomerByEmail(this.em);
                merch.Inventory = this.inventory_quantity;
                return cust;
            } */
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

           /* public Merchandise[] GetVar()
            {
                Merchandise[] merch = new Merchandise[variants.Count];
                if (variants.Count > 0)
                {
                    for (int i = 0; i < variants.Count; i++)
                    {
                        // Console.WriteLine(i);
                        merch[i] = variants[i].GetValues();
                    }
                }
                return merch;
            }*/
        }

        public class RootObject
        {
            public List<Customers> customers { get; set; }

            /*public Merchandise[][] GetMerchandise()
            {
                Merchandise[][] merch = new Merchandise[customers.Count][];
                for (int i = 0; i < customers.Count; i++)
                {
                    merch[i] = this.customers[i].GetVar();
                }
                return merch;
            }*/
        }
    }
}

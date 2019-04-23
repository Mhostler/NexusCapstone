using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    public class Line_Item
    {
        public Line_Item()
        {

        }

        public Line_Item(object id, object variant_id, string name, int quantity, string sku, string price)
        {
            this.id = id;
            this.variant_id = variant_id;
            this.name = name;
            this.quantity = quantity;
            this.sku = sku;
            this.price = price;
        }
        public object id { get; set; }
        public object variant_id { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public string sku { get; set; }
        public string variant_title { get; set; }
        public string vendor { get; set; }
        public string fulfillment_service { get; set; }
        public object product_id { get; set; }
        public Boolean requires_shipping { get; set; }
        public Boolean taxable { get; set; }
        public Boolean gift_card { get; set; }
        public string name { get; set; }
        public string variant_inventory_management { get; set; }
        public List<Note_Attribute> properties { get; set; }
        public Boolean product_exists { get; set; }
        public int fulfillable_quantity { get; set; }
        public int? grams { get; set; }
        public string price { get; set; }
        public string total_discount { get; set; }
        public string fulfillment_status { get; set; }
        public Price_Set price_set { get; set; }
        public Price_Set total_discount_set { get; set; }
        public List<Discount_Allocation> discount_allocations { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<Tax_Line> tax_lines { get; set; }

        public TransactionItem getItem()
        {
            TransactionItem item = new TransactionItem();
            int number;
            decimal dnum;
            int.TryParse(sku, out number);
            Decimal.TryParse(price, out dnum);
            item.ItemID = number;
            item.Name = name;
            item.Quantity = quantity;
            item.Price = dnum;
            return item;
        }
    }

    public class Refund_Line_Item
    {
        public object id { get; set; }
        public int quantity { get; set; }
        public object line_item_id { get; set; }
        public object location_id { get; set; }
        public string restock_type { get; set; }
        public double subtotal { get; set; }
        public double total_tax { get; set; }
        public Price_Set subtotal_set { get; set; }
        public Price_Set total_tax_set { get; set; }
        public Line_Item line_item { get; set; }
    }

    public class Discount_Allocation
    {
        public string amount { get; set; }
        public int discount_application_index { get; set; }
        public Price_Set amount_set { get; set; }
    }

    public class Discount_Application
    {
        public string type { get; set; }
        public string value { get; set; }
        public string value_type { get; set; }
        public string allocation_method { get; set; }
        public string target_selection { get; set; }
        public string target_type { get; set; }
        public string code { get; set; }
    }

    public class Discount_Code
    {
        public string code { get; set; }
        public string amount { get; set; }
        public string percentage { get; set; }
    }

    public class Note_Attribute
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Price
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class Price_Set
    {
        public Price shop_money { get; set; }
        public Price presentment_money { get; set; }
    }

    public class Tax_Line
    {
        public string price { get; set; }
        public double rate { get; set; }
        public string title { get; set; }
        public Price_Set price_set { get; set; }
    }

    public class Receipt
    {
        public Boolean testcase { get; set; }
        public string authorization { get; set; }
    }

    public class Shipping_Line
    {
        public object id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string code { get; set; }
        public string source { get; set; }
        public string phone { get; set; }
        public string requested_fulfillment_service_id { get; set; }
        public string delivery_category { get; set; }
        public string carrier_identifier { get; set; }
        public string discounted_price { get; set; }
        public Price_Set price_set { get; set; }
        public Price_Set discounted_price_set { get; set; }
        public List<Discount_Allocation> discount_allocations { get; set; }
        public List<Tax_Line> tax_lines { get; set; }
    }

    public class Address
    {
        public string first_name { get; set; }
        public string address1 { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string last_name { get; set; }
        public string address2 { get; set; }
        public string company { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public string province_code { get; set; }
    }

    public class Fulfillment
    {
        public object id { get; set; }
        public object order_id { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string service { get; set; }
        public string updated_at { get; set; }
        public string tracking_company { get; set; }
        public string shipment_status { get; set; }
        public object location_id { get; set; }
        public string tracking_number { get; set; }
        public List<String> tracking_numbers { get; set; }
        public string tracking_url { get; set; }
        public List<String> tracking_urls { get; set; }
        public Receipt receipt { get; set; }
        public string name { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<Line_Item> line_items { get; set; }
    }

    public class Client_Details
    {
        public string browser_ip { get; set; }
        public string accept_language { get; set; }
        public string user_agent { get; set; }
        public string session_hash { get; set; }
        public string browser_width { get; set; }
        public string browser_height { get; set; }
    }

    public class Order_Adjustment
    {
        public object id { get; set; }
        public object order_id { get; set; }
        public object refund_id { get; set; }
        public string amount { get; set; }
        public string tax_amount { get; set; }
        public string reason { get; set; }
        public Price_Set amount_set { get; set; }
        public Price_Set tax_amount_set { get; set; }
    }

    public class Refund
    {
        public object id { get; set; }
        public object order_id { get; set; }
        public string created_at { get; set; }
        public string note { get; set; }
        public object user_id { get; set; }
        public string processed_at { get; set; }
        public Boolean restock { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<Refund_Line_Item> refund_line_items { get; set; }
        public List<Transactions> transactions { get; set; }
        public List<Order_Adjustment> order_adjustments { get; set; }
    }

    public class Transactions
    {
        public object id { get; set; }
        public object order_id { get; set; }
        public string kind { get; set; }
        public string gateway { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string created_at { get; set; }
        public Boolean test { get; set; }
        public string authorization { get; set; }
        public object location_id { get; set; }
        public object user_id { get; set; }
        public object parent_id { get; set; }
        public string processed_at { get; set; }
        public object device_id { get; set; }
        public Receipt receipt { get; set; }
        public string error_code { get; set; }
        public string source_name { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string admin_graphql_api_id { get; set; }
    }

    public class Payment_Details
    {
        public string credit_card_bin { get; set; }
        public string avs_result_code { get; set; }
        public string cvv_result_code { get; set; }
        public string credit_card_number { get; set; }
        public string credit_card_company { get; set; }
    }

    public class Orders
    {
        public object id { get; set; }
        public string email { get; set; }
        public string closed_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int number { get; set; }
        public string note { get; set; }
        public string token { get; set; }
        public string gateway { get; set; }
        public Boolean test { get; set; }
        public string total_price { get; set; }
        public string subtotal_price { get; set; }
        public int total_weight { get; set; }
        public string total_tax { get; set; }
        public Boolean taxes_included { get; set; }
        public string currency { get; set; }
        public string financial_status { get; set; }
        public Boolean confirmed { get; set; }
        public string total_discounts { get; set; }
        public string total_line_items_price { get; set; }
        public string cart_token { get; set; }
        public Boolean buyer_accepts_marketing { get; set; }
        public string name { get; set; }
        public string referring_site { get; set; }
        public string landing_site { get; set; }
        public string cancelled_at { get; set; }
        public string cancel_reason { get; set; }
        public string total_price_usd { get; set; }
        public string checkout_token { get; set; }
        public string reference { get; set; }
        public string user_id { get; set; }
        public string location_id { get; set; }
        public string source_identifier { get; set; }
        public string source_url { get; set; }
        public string processed_at { get; set; }
        public string device_id { get; set; }
        public string phone { get; set; }
        public string customer_locale { get; set; }
        public string app_id { get; set; }
        public string browser_ip { get; set; }
        public string landing_site_ref { get; set; }
        public int order_number { get; set; }
        public List<Discount_Application> discount_applications { get; set; }
        public List<Discount_Code> discount_codes { get; set; }
        public List<Note_Attribute> note_attributes { get; set; }
        public List<String> payment_gateway_names { get; set; }
        public string processing_method { get; set; }
        public object checkout_id { get; set; }
        public string source_name { get; set; }
        public string fulfillment_status { get; set; }
        public List<Tax_Line> tax_lines { get; set; }
        public string tags { get; set; }
        public string contact_email { get; set; }
        public string order_status_url { get; set; }
        public string presentment_currency { get; set; }
        public Price_Set total_line_items_price_set { get; set; }
        public Price_Set total_discounts_set { get; set; }
        public Price_Set total_shipping_price_set { get; set; }
        public Price_Set subtotal_price_set { get; set; }
        public Price_Set total_price_set { get; set; }
        public Price_Set total_tax_set { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<Line_Item> line_items { get; set; }
        public List<Shipping_Line> shipping_lines { get; set; }
        public Address billing_address { get; set; }
        public Address shipping_address { get; set; }
        public List<Fulfillment> fulfillments { get; set; }
        public Client_Details client_details { get; set; }
        public List<Refund> refunds { get; set; }
        public Payment_Details payment_details { get; set; }
        public Customers customer { get; set; }

        public Transaction getTran()
        {
            Transaction tran = new Transaction();
            Transaction last = new Transaction();
            last.TransactionID = DBHandler.SelectLastOrder();
            tran.TransactionID = order_number;
            if (tran.TransactionID <= last.TransactionID) // if transaction is in database, skip
            {
                tran.Cust = customer.GetCust();
                tran.TList = getItems();
                tran.InsertTransaction();
            }
            return tran;
        }

        public List<TransactionItem> getItems()
        {
            List<TransactionItem> items = new List<TransactionItem>();
            for (int i = 0; i < line_items.Capacity; i++)
            {
                items.Add(line_items[0].getItem());
            }
            return items;
        }
    }

    public class RootObject2
    {
        public List<Orders> transactions;

        public Transaction[] GetTransactions()
        {
            if (transactions != null)
            {
                Transaction[] trans = new Transaction[transactions.Count];
                for (int i = 0; i < transactions.Count; i++)
                {
                    trans[i] = transactions[i].getTran();
                }
                return trans;
            } else
            {
                return null;
            }
        }

        public Transaction[] GetTransactions(DateTime dateTime)
        {
            if (transactions != null)
            {
                Transaction[] trans = new Transaction[transactions.Count];
                for (int i = 0; i < transactions.Count; i++)
                {
                    trans[i] = transactions[i].getTran();
                }
                return trans;
            }
            else
            {
                return null;
            }
        }
    }
}

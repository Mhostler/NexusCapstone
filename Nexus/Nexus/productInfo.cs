using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{

    public class Variant
    {
        public Variant() {

        }

        public Variant(object id, object product_id, string title, string price, string sku, string option1, string option2, int inventory_quantity)
        {
            this.id = id;
            this.product_id = product_id;
            this.title = title;
            this.price = price;
            this.sku = sku;
            this.option1 = option1;
            this.option2 = option2;
            this.inventory_quantity = inventory_quantity;

        }
        public object id { get; set; }
        public object product_id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string sku { get; set; }
        public int position { get; set; }
        public int? grams { get; set; }
        public string inventory_policy { get; set; }
        public object compare_at_price { get; set; }
        public string fulfillment_service { get; set; }
        public string inventory_management { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public object option3 { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public bool taxable { get; set; }
        public string barcode { get; set; }
        public object image_id { get; set; }
        public int inventory_quantity { get; set; }
        public double weight { get; set; }
        public string weight_unit { get; set; }
        public int old_inventory_quantity { get; set; }
        public bool requires_shipping { get; set; }

        public Merchandise GetValues()
        {
            Merchandise merch = new Merchandise();
            int num;
            int.TryParse(this.sku, out num);
            merch.ItemID = num;
            merch.Name = this.title;
            // merch.Price = this.price;
            merch.Inventory = this.inventory_quantity;
            return merch;
        }
    }

    public class Option
    {
        public object id { get; set; }
        public object product_id { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public List<string> values { get; set; }
    }

    public class Image
    {
        public object id { get; set; }
        public object product_id { get; set; }
        public int position { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string src { get; set; }
        public List<object> variant_ids { get; set; }
    }

    public class Image2
    {
        public object id { get; set; }
        public object product_id { get; set; }
        public int position { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string src { get; set; }
        public List<object> variant_ids { get; set; }
    }

    public class Product
    {
        public object id { get; set; }
        public string title { get; set; }
        public string body_html { get; set; }
        public string vendor { get; set; }
        public string product_type { get; set; }
        public string created_at { get; set; }
        public string handle { get; set; }
        public string updated_at { get; set; }
        public string published_at { get; set; }
        public string template_suffix { get; set; }
        public string published_scope { get; set; }
        public string tags { get; set; }
        public List<Variant> variants { get; set; }
        public List<Option> options { get; set; }
        public List<Image> images { get; set; }
        public Image2 image { get; set; }

        public Merchandise[] GetVar()
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
        }
    }

    public class RootObject
    {
        public List<Product> products { get; set; }

        public Merchandise[][] GetMerchandise()
        {
            Merchandise[][] merch = new Merchandise[products.Count][];
            for (int i = 0; i < products.Count; i++)
            {
                merch[i] = this.products[i].GetVar();
            }
            return merch;
        }
    }

    
}

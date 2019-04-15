using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    public class Merchandise
    {
        public int ItemID { get; set; }
        public String Name { get; set; }
        public String Size { get; set; }
        public int Inventory { get; set; }
        public decimal Price { get; set; }

        public Merchandise()
        {
            ItemID = 0;
            Name = "";
            Size = "";
            Inventory = 0;
            Price = 0;
        }
        public Merchandise(Merchandise m)
        {
            ItemID = m.ItemID;
            Name = m.Name;
            Size = m.Size;
            Inventory = m.Inventory;
            Price = m.Price;
        }

        public Merchandise(String name, String size, int inventory, decimal price, int id = 0)
        {
            ItemID = id;
            Name = name;
            Size = size;
            Inventory = inventory;
            Price = price;
        }

        public void SetMerchandise(Merchandise m)
        {
            ItemID = m.ItemID;
            Name = m.Name;
            Size = m.Size;
            Inventory = m.Inventory;
            Price = m.Price;
        }

        public void Update()
        {
            string n = Name.Replace("\'", "\\'");
            string s = Size.Replace("\'", "\\'");
            string query = "UPDATE Merch SET Name='" + n + "', Size='" + s + "', Inventory=" + Inventory.ToString() +
                ", Price=" + Price.ToString() + " WHERE ItemID=" + ItemID.ToString();
            DBHandler.ExecuteNoReturn(query);
        }

        public void InsertItem()
        {
            string n = Name.Replace("\'", "\\'");
            string s = Size.Replace("\'", "\\'");
            string query = "UPDATE Merch SET Name='" + n + "', Size='" + s + "', Inventory=" + Inventory.ToString() +
                ", Price=" + Price.ToString() + " WHERE ItemID=" + ItemID.ToString();
            DBHandler.ExecuteNoReturn(query);
        }
    }
}

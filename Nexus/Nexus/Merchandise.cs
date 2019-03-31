using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class Merchandise
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
            price = Price;
        }

        public void InsertItem()
        {
            string query = "INSERT INTO Merch (Name, Size, Inventory, Price) VALUES('" + Name + "', '" +
                Size + "', " + Inventory.ToString() + ", " + Price.ToString() + ")";

            DBHandler.ExecuteNoReturn(query);
        }
    }
}

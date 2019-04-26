using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// represents a merchandise item
    /// </summary>
    public class Merchandise
    {
        public int ItemID { get; set; }
        public String Name { get; set; }
        public String Size { get; set; }
        public int Inventory { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// default constructor sets values to zero
        /// </summary>
        public Merchandise()
        {
            ItemID = 0;
            Name = "";
            Size = "";
            Inventory = 0;
            Price = 0;
        }

        /// <summary>
        /// constructor copies values from another merchandise item
        /// </summary>
        /// <param name="m">merchandise to copy from</param>
        public Merchandise(Merchandise m)
        {
            ItemID = m.ItemID;
            Name = m.Name;
            Size = m.Size;
            Inventory = m.Inventory;
            Price = m.Price;
        }

        /// <summary>
        /// constructor creates merchandise given one of each field
        /// </summary>
        /// <param name="name">name of merchandise</param>
        /// <param name="size">size string of merchandise</param>
        /// <param name="inventory">amount of stock in the store</param>
        /// <param name="price">store price for item</param>
        /// <param name="id">id within the database</param>
        public Merchandise(String name, String size, int inventory, decimal price, int id = 0)
        {
            ItemID = id;
            Name = name;
            Size = size;
            Inventory = inventory;
            Price = price;
        }

        /// <summary>
        /// copies values from another merchandise
        /// </summary>
        /// <param name="m">merchandise to copy</param>
        public void SetMerchandise(Merchandise m)
        {
            ItemID = m.ItemID;
            Name = m.Name;
            Size = m.Size;
            Inventory = m.Inventory;
            Price = m.Price;
        }

        /// <summary>
        /// updates value in the db
        /// </summary>
        public void Update()
        {
            string n = Name.Replace("\'", "\\'");
            string s = Size.Replace("\'", "\\'");
            string query = "UPDATE Merch SET Name='" + n + "', Size='" + s + "', Inventory=" + Inventory.ToString() +
                ", Price=" + Price.ToString() + " WHERE ItemID=" + ItemID.ToString();
            DBHandler.ExecuteNoReturn(query);
        }

        /// <summary>
        /// inserts merchandise into the database
        /// </summary>
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

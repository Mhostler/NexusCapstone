﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// represents an order of new vendor items
    /// </summary>
    class Order
    {
        public int OrderID { get; set; }
        public Vendor OrderVendor { get; set; }
        public DateTime Placed { get; set; }
        public DateTime Received { get; set; }
        public List<OrderItem> items;
        private decimal total;
        public decimal Total { get { return total; } }

        /// <summary>
        /// default constructor sets values to zero
        /// </summary>
        public Order()
        {
            items = new List<OrderItem>();
            Placed = new DateTime();
            Received = new DateTime();
            total = 0;
        }

        /// <summary>
        /// adds an item into the order and its cost to the total
        /// </summary>
        /// <param name="vi">the item from the vendor to order</param>
        /// <param name="quantity">the amount of items to purchase</param>
        public void addItem(VendorItem vi, int quantity)
        {
            OrderItem newItem = new OrderItem(vi, quantity);

            items.Add(newItem);

            total += newItem.UnitPrice * newItem.Quantity;
        }

        /// <summary>
        /// inserts order and items into the database
        /// </summary>
        public void InsertOrder()
        {
            String orderQuery = "INSERT INTO Orders (VendorID, Placed, Received) VALUES (" +
                OrderVendor.Id.ToString() + ", '" + Placed.ToString("yyyy-MM-dd") + "', '" + Received.ToString("yyyy-MM-dd") + "')";
            String[] itemQueries = new string[items.Count];
            OrderItem[] oItem = items.ToArray();

            DBHandler.ExecuteNoReturn(orderQuery);

            for (int i = 0; i < items.Count; i++)
            {
                itemQueries[i] = "INSERT INTO OrderItems (Quantity, vmID, OrderID ) VALUES (" +
                    oItem[i].Quantity.ToString() + ", " +
                    oItem[i].Vmid.ToString() + ", (select max(OrderID) from Orders))";
            }

            DBHandler.ExecuteMultipleNoReturn(itemQueries);
        }

        /// <summary>
        /// updates order and orderitem values in the database
        /// </summary>
        public void UpdateOrder()
        {
            string OrderQuery = "UPDATE Orders SET VendorID=" + OrderVendor.Id.ToString() + ", " +
                "Placed='" + Placed.ToString("yyyy-MM-dd") + "', Received='" + Received.ToString("yyyy-MM-dd") +
                "' WHERE OrderID=" + OrderID.ToString();

            DBHandler.ExecuteNoReturn(OrderQuery);

            string[] itemQueries = new string[items.Count];
            for(int i = 0; i < items.Count; i++)
            {
                itemQueries[i] = "UPDATE OrderItems SET Quantity=" + items[i].Quantity.ToString() +
                    " WHERE oID=" + items[i].Oid.ToString();
            }

            DBHandler.ExecuteMultipleNoReturn(itemQueries);
        }

        /// <summary>
        /// recalculate the total
        /// </summary>
        /// <returns>total value of order</returns>
        public decimal CalcTotal()
        {
            decimal tot = 0.0M;
            foreach (OrderItem item in items)
            {
                tot += item.getPrice();
            }
            total = tot;
            return total;
        }
    }
}

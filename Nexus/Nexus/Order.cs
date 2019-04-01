using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class Order
    {
        public int OrderID { get; set; }
        public Vendor OrderVendor { get; set; }
        public List<OrderItem> items;
        private decimal total;
        public decimal Total { get; }

        public Order()
        {
            items = new List<OrderItem>();
            total = 0;
        }

        public void addItem(VendorItem vi, int quantity)
        {
            OrderItem newItem = new OrderItem();
            items.Add(newItem);

            total += newItem.UnitPrice * newItem.Quantity;
        }

        public void InsertOrder()
        {
            //TODO select statement to get proper ID
            DateTime today = DateTime.Now;
            String date = today.ToString("yyyy-MM-dd");

            String orderQuery = "INSERT INTO Orders (VendorID, Placed) VALUES ('" +
                OrderVendor.Id.ToString() + "', " + date + "')";
            String[] itemQueries = new string[items.Count];
            OrderItem[] oItem = items.ToArray();

            DBHandler.ExecuteNoReturn(orderQuery);
            OrderID = DBHandler.SelectLastOrder();

            OrderID = DBHandler.SelectMostRecentOrder(OrderVendor.Id, today);

            for (int i = 0; i < items.Count; i++)
            {
                itemQueries[i] = "INSERT INTO OrderItems (Quantity, Price, vmID, OrderID) VALUES (" +
                    oItem[i].Quantity.ToString() + ", " + oItem[i].Price + ", " +
                    oItem[i].Vmid + ", " + OrderID.ToString() + ")";
            }

            DBHandler.ExecuteMultipleNoReturn(itemQueries);
        }

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

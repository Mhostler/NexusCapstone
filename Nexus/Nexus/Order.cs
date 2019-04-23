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
        public DateTime Placed { get; set; }
        public DateTime Received { get; set; }
        public List<OrderItem> items;
        private decimal total;
        public decimal Total { get { return total; } }

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
            //String date = today.ToString("yyyy-MM-dd");

            String orderQuery = "INSERT INTO Orders (VendorID, Placed, Received) VALUES (" +
                OrderVendor.Id.ToString() + ", '" + Placed + "', '" + Received + "')";
            String[] itemQueries = new string[items.Count];
            OrderItem[] oItem = items.ToArray();

            DBHandler.ExecuteNoReturn(orderQuery);

            for (int i = 0; i < items.Count; i++)
            {
                itemQueries[i] = "INSERT INTO OrderItems (Quantity, Price, vmID, (select max(OrderID) from Orders)) VALUES (" +
                    oItem[i].Quantity.ToString() + ", " + oItem[i].Price + ", " +
                    oItem[i].Vmid + ", " + OrderID.ToString() + ")";
            }

            DBHandler.ExecuteMultipleNoReturn(itemQueries);
        }

        public void UpdateOrder()
        {
            string OrderQuery = "UPDATE Orders SET VendorID=" + OrderVendor.Id.ToString() + ", " +
                "Placed='" + Placed.ToString("yyyy-MM-dd") + "', Received='" + Received.ToString("yyyy-MM-dd") +
                "WHERE OrderID=" + OrderID.ToString();

            DBHandler.ExecuteNoReturn(OrderQuery);

            string[] itemQueries = new string[items.Count];
            for(int i = 0; i < items.Count; i++)
            {
                itemQueries[i] = "UPDATE OrderItems Quantity=" + items[i].Quantity.ToString() +
                    " WHERE oID=" + items[i].Oid.ToString();
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

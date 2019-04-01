using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class OrderItem : VendorItem
    {
        public int Oid { get; set; }
        public int Quantity { get; set; }

        public OrderItem() : base()
        {
            Quantity = 0;
        }

        public OrderItem(OrderItem oi) : base((Merchandise)oi, oi.VendorID, oi.UnitSize, oi.UnitPrice, oi.Vmid)
        {
            Quantity = oi.Quantity;
        }

        public OrderItem(VendorItem vi, int quantity, int oid = 0) : base(vi)
        {
            Oid = oid;
            Quantity = quantity;
        }

        public void SetOrderItem(OrderItem oi)
        {
            SetVendorItem(oi);
            Oid = oi.Oid;
            Quantity = oi.Quantity;
        }

        public decimal getPrice()
        {
            return this.UnitPrice * Quantity;
        }
    }
}

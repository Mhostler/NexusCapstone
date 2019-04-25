using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// orderitem decends from vendor items represents items to buy from vendor
    /// </summary>
    class OrderItem : VendorItem
    {
        public int Oid { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// default constructor just sets quantity to zero
        /// </summary>
        public OrderItem() : base()
        {
            Quantity = 0;
        }

        /// <summary>
        /// constructor copies orderitem
        /// </summary>
        /// <param name="oi">orderitem to copy</param>
        public OrderItem(OrderItem oi) : base((Merchandise)oi, oi.VendorID, oi.UnitSize, oi.UnitPrice, oi.Vmid)
        {
            Quantity = oi.Quantity;
        }

        /// <summary>
        /// copies order values from a vendor and sets amount to purchase
        /// </summary>
        /// <param name="vi">vendor item being purchased</param>
        /// <param name="quantity">amount to purchase</param>
        /// <param name="oid">IDo f the orderitem</param>
        public OrderItem(VendorItem vi, int quantity, int oid = 0) : base(vi)
        {
            Oid = oid;
            Quantity = quantity;
        }

        /// <summary>
        /// copies from an orderitem
        /// </summary>
        /// <param name="oi">order item to copy</param>
        public void SetOrderItem(OrderItem oi)
        {
            SetVendorItem(oi);
            Oid = oi.Oid;
            Quantity = oi.Quantity;
        }

        /// <summary>
        /// returns the price value of the item
        /// </summary>
        /// <returns>total price</returns>
        public decimal getPrice()
        {
            return this.UnitPrice * Quantity;
        }
    }
}

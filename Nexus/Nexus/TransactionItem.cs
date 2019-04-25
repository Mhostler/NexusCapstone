using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// Transaction item is a child of merchandise
    /// contains a quantity for amount of merch being bought
    /// </summary>
    public class TransactionItem : Merchandise
    {
        public int tID { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// default constructor zeroes values
        /// </summary>
        public TransactionItem() : base()
        {
            tID = 0;
            Quantity = 0;
        }

        /// <summary>
        /// constructor that copies from another transaction item
        /// </summary>
        /// <param name="t">item to copy</param>
        public TransactionItem(TransactionItem t)
        {
            SetMerchandise(t);
            tID = t.tID;
            Quantity = t.Quantity;
        }

        /// <summary>
        /// returns the total price of the order
        /// </summary>
        /// <returns>total value of order</returns>
        public decimal GetTotalPrice()
        {
            return Quantity * Price;
        }

        /// <summary>
        /// copies the transaction from another
        /// </summary>
        /// <param name="ti">item to copy</param>
        public void SetTransactionitem(TransactionItem ti)
        {
            SetMerchandise(ti);
            tID = ti.tID;
            Quantity = ti.Quantity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    public class TransactionItem : Merchandise
    {
        public int tID { get; set; }
        public int Quantity { get; set; }

        public TransactionItem() : base()
        {
            tID = 0;
            Quantity = 0;
        }

        public TransactionItem(TransactionItem t)
        {
            SetMerchandise(t);
            tID = t.tID;
            Quantity = t.Quantity;
        }

        public decimal GetTotalPrice()
        {
            return Quantity * Price;
        }

        public void SetTransactionitem(TransactionItem ti)
        {
            SetMerchandise(ti);
            tID = ti.tID;
            Quantity = ti.Quantity;
        }
    }
}

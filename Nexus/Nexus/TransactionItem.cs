using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class TransactionItem : Merchandise
    {
        public int tID { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return Quantity * Price;
        }
    }
}

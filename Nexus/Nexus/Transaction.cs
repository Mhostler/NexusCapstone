using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class Transaction
    {
        public int TransactionID { get; set; }
        public Customer Cust { get; set; }
        public List<TransactionItem> TList;
        public decimal Total { get; }

        public Transaction()
        {
            TList = new List<TransactionItem>();
        }

        public void addItem(Merchandise m)
        {

        }

        public void InsertTransaction()
        {
            //TODO set ID = to newly inserted transaction
            String query = "INSERT INTO Transaction (CustID) VALUES (" + Cust.Id + ")";
            DBHandler.ExecuteNoReturn(query);

            String[] itemQueries = new string[TList.Count];
            TransactionItem[] items = TList.ToArray();

            for(int i = 0; i < TList.Count; i++)
            {
                itemQueries[i] = "INSERT INTO TItem (TransactionID, ItemID, Quantity) VALUES (" +
                    TransactionID.ToString() + ", " + items[i].ItemID.ToString() + ", " +  items[i].Quantity + ")";
            }

            DBHandler.ExecuteMultipleNoReturn(itemQueries);
        }
    }
}

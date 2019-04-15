using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
     public class Transaction
    {
        public int TransactionID { get; set; }
        public Customer Cust { get; set; }
        public List<TransactionItem> TList { get; set; }
        public decimal Total { get; }

        public Transaction()
        {
            TList = new List<TransactionItem>();
            Cust = new Customer();
            TransactionID = 0;
            Total = 0.0M;
        }

        public void AddItem(Merchandise m, int quantity)
        {
            TransactionItem t = new TransactionItem();
            t.SetMerchandise(m);
            t.Quantity = quantity;
            TList.Add(t);
        }

        public void InsertTransaction()
        {
            //TODO set ID = to newly inserted transaction
            String query = "INSERT INTO Transaction (CustID) VALUES (" + Cust.Id + ")";
            DBHandler.ExecuteNoReturn(query);

            TransactionID = DBHandler.SelectMostRecentTransaction(Cust.Id);
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

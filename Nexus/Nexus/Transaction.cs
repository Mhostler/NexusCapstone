﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// Transactions represent customers buying from our business
    /// </summary>
     public class Transaction
    {
        public int TransactionID { get; set; }
        public Customer Cust { get; set; }
        public DateTime Day { get; set; }
        private List<TransactionItem> tList;
        public List<TransactionItem> TList
        {
            get
            {
                return this.tList;
            }
            set
            {
                this.tList = value;
                Total = 0;
                foreach(TransactionItem t in this.tList)
                {
                    Total += t.Price * t.Quantity;
                }
            }
        }
        public decimal Total { get; protected set; }

        /// <summary>
        /// default constructor sets to zero values
        /// </summary>
        public Transaction()
        {
            TList = new List<TransactionItem>();
            Day = DateTime.Now;
            Cust = new Customer();
            TransactionID = 0;
            Total = 0.0M;
        }

        /// <summary>
        /// adds an item to the transaction
        /// </summary>
        /// <param name="m">item to add</param>
        /// <param name="quantity">amount of items being purchased</param>
        public void AddItem(Merchandise m, int quantity)
        {
            TransactionItem t = new TransactionItem();
            t.SetMerchandise(m);
            t.Quantity = quantity;
            Total += m.Price * quantity;
            TList.Add(t);
        }

        /// <summary>
        /// inserts a transaction into the database
        /// </summary>
        public void InsertTransaction()
        {
            //TODO set ID = to newly inserted transaction
            String query = "INSERT INTO Transactions (CustID, Day) VALUES (" + Cust.Id + ", '" + Day.ToString("yyyy-MM-dd H:mm:ss") + "')";
            
            //DBHandler.ExecuteNoReturn(query);
            //TransactionID = DBHandler.getLastTransactionID();
            
            String[] itemQueries = new string[TList.Count + 1];
            itemQueries[0] = query;
            TransactionItem[] items = TList.ToArray();

            for(int i = 0; i < TList.Count; i++)
            {
                itemQueries[i + 1] = "INSERT INTO TItem (TransactionID, ItemID, Quantity) VALUES ( (SELECT max(TransactionID) " +
                    "FROM Transactions), " + items[i].ItemID.ToString() + ", " +  items[i].Quantity + ")";
            }

            DBHandler.ExecuteMultipleNoReturn(itemQueries);
        }
    }
}

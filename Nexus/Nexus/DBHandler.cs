using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Nexus
{
    static class DBHandler
    {
        private static readonly string server = "nexusgifts.c79k1z6krivv.us-east-2.rds.amazonaws.com";
        private static readonly string database = "NexusDB";
        private static readonly string user = "Nexus";
        private static readonly string pass = "Capstone2019";
        private static readonly string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + user + ";" + "PASSWORD=" + pass + ";";
        private static MySqlConnection connection = new MySqlConnection(connectionString);

        private static readonly string EarningsTable = "DailyEarnings";
        private static readonly string TestEarningsTable = "TestEarnings";

        private static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    //cannot connect error
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    //invalid username/password
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
                throw;
            }
        }

        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static void ExecuteNoReturn(String query)
        {
            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        public static void ExecuteMultipleNoReturn(String [] queries)
        {
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = connection
                };
                
                foreach (String query in queries)
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }

                CloseConnection();
            }
        }

        public static int SelectMostRecentOrder(int VendorID, DateTime date)
        {
            String d = date.ToString("yyyy-MM-dd");
            List<int> list = new List<int>();

            String query = "SELECT OrderID FROM Orders WHERE VendorID=" + VendorID.ToString() + " AND Placed=" + d;

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    list.Add((int)reader["VendorID"]);
                }

                reader.Close();
                CloseConnection();

                return list.Last();
            }

            return -1;
        }

        public static decimal getOrderTotalByRange(DateTime start, DateTime end)
        {
            string query = "SELECT OrderItems.Quantity as Quant, VendorMerch.UnitPrice as Price " +
                "from ((Orders inner join OrderItems on Orders.OrderID=OrderItems.OrderID) " +
                "inner join VendorMerch on OrderItems.vmID=VendorMerch.vmID) " +
                "Where Orders.Placed>'" + start.ToString("yyyy-MM-dd") + "' and Orders.Placed<'" +
                end.ToString("yyyy-MM-dd") + "'";
            decimal total = 0;

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    total += decimal.Parse(reader["Quant"] + "") * decimal.Parse(reader["Price"] + "");
                }

                reader.Close();
                CloseConnection();
            }

            return total;
        }

        public static int SelectMostRecentTransaction(int CustID)
        {
            List<int> list = new List<int>();

            String query = "SELECT TransactionID FROM Transactions WHERE CustID=" + CustID.ToString();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    list.Add((int)reader["TransactionID"]);
                }

                reader.Close();
                CloseConnection();

                return list.Last();
            }

            return -1;
        }

        public static List<VendorItem> SelectVendorItems(Vendor vend)
        {
            List<VendorItem> list = new List<VendorItem>();

            String query = "SELECT * FROM " +
                "(VendorMerch INNER JOIN Merch ON VendorMerch.ItemID=Merch.ItemID) " +
                "WHERE VendorMerch.VendorID=" + vend.Id.ToString();

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    VendorItem v = new VendorItem
                    {
                        Vmid = Int32.Parse(reader["vmID"] + ""),
                        ItemID = Int32.Parse(reader["ItemID"] + ""),
                        Name = reader["Name"] + "",
                        Size = reader["Size"] + "",
                        Price = Decimal.Parse(reader["Price"] + ""),
                        Inventory = Int32.Parse(reader["Inventory"] + ""),
                        VendorID = Int32.Parse(reader["VendorID"] + ""),
                        UnitSize = Int32.Parse(reader["UnitSize"] + ""),
                        UnitPrice = Decimal.Parse(reader["UnitPrice"] + "")
                    };
                    list.Add(v);
                }

                reader.Close();
                CloseConnection();
            }

            return list;
        }

        public static int SelectLastOrder()
        {
            string query = "SELECT MAX(OrderID) from Orders";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int id = Int32.Parse(reader["OrderID"] + "");
                reader.Close();
                CloseConnection();
                return id;
            }
            return -1;
        }

        public static List<Order> getAllOrders()
        {
            List<Order> oList = new List<Order>();
            string query = "SELECT * FROM Orders";

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order o = new Order();
                    o.OrderID = Int32.Parse(reader["OrderID"] + "");
                    o.Placed = DateTime.Parse(reader["Placed"] + "");
                    o.Received = DateTime.Parse(reader["Received"] + "");

                    oList.Add(o);
                }

                for(int i = 0; i < oList.Count; i++)
                {
                    reader.Close();

                    query = "select " +
                        "OrderItems.oID as oID, " +
                        "OrderItems.Quantity as quant, " +
                        "OrderItems.vmID as vmID, " +
                        "VendorMerch.VendorID as VendorID, " +
                        "VendorMerch.ItemID as ItemID, " +
                        "VendorMerch.UnitSize as UnitSize, " +
                        "VendorMerch.UnitPrice as UnitPrice, " +
                        "Merch.Name as Name, " +
                        "Merch.Size as Size, " +
                        "Merch.Inventory as Inv, " +
                        "Merch.Price as Price " +
                        "FROM ((" +
                        "OrderItems INNER JOIN VendorMerch on OrderItems.vmID=VendorMerch.vmID) " +
                        "INNER JOIN Merch on VendorMerch.ItemID = Merch.ItemID) " +
                        "WHERE OrderItems.OrderID=" + oList[i].OrderID.ToString();
                    cmd.CommandText = query;

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Merchandise m = new Merchandise
                        {
                            Name = reader["Name"] + "",
                            Size = reader["Size"] + "",
                            Inventory = Int32.Parse(reader["Inv"] + ""),
                            Price = decimal.Parse(reader["Price"] + "")
                        };
                        VendorItem vi = new VendorItem(m,
                            Int32.Parse(reader["vmID"] + ""),
                            Int32.Parse(reader["UnitSize"] + ""),
                            decimal.Parse(reader["UnitPrice"] + ""),
                            Int32.Parse(reader["VendorID"] + ""));

                        oList[i].addItem(vi, Int32.Parse(reader["quant"] + ""));
                    }
                }

                reader.Close();
                CloseConnection();
            }

            return oList;
        }

        public static List<Order> getOrderByRange(DateTime start, DateTime end)
        {
            List<Order> oList = new List<Order>();

            string query = "SELECT * FROM Orders WHERE Placed>='" + start.ToString("yyyy-MM-dd") +
                "' AND Placed<='" + end.ToString("yyyy-MM-dd") + "'";

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order o = new Order
                    {
                        OrderID = Int32.Parse(reader["OrderID"] + ""),
                        Placed = DateTime.Parse(reader["Placed"] + ""),
                        Received = DateTime.Parse(reader["Received"] + "")
                    };

                    oList.Add(o);
                }

                for (int i = 0; i < oList.Count; i++)
                {
                    reader.Close();

                    query = "select " +
                        "OrderItems.oID as oID, " +
                        "OrderItems.Quantity as quant, " +
                        "OrderItems.vmID as vmID, " +
                        "VendorMerch.VendorID as VendorID, " +
                        "VendorMerch.ItemID as ItemID, " +
                        "VendorMerch.UnitSize as UnitSize, " +
                        "VendorMerch.UnitPrice as UnitPrice, " +
                        "Merch.Name as Name, " +
                        "Merch.Size as Size, " +
                        "Merch.Inventory as Inv, " +
                        "Merch.Price as Price " +
                        "FROM ((" +
                        "OrderItems INNER JOIN VendorMerch on OrderItems.vmID=VendorMerch.vmID) " +
                        "INNER JOIN Merch on VendorMerch.ItemID = Merch.ItemID) " +
                        "WHERE OrderItems.OrderID=" + oList[i].OrderID.ToString();
                    cmd.CommandText = query;

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Merchandise m = new Merchandise
                        {
                            Name = reader["Name"] + "",
                            Size = reader["Size"] + "",
                            Inventory = Int32.Parse(reader["Inv"] + ""),
                            Price = decimal.Parse(reader["Price"] + "")
                        };
                        VendorItem vi = new VendorItem(m,
                            Int32.Parse(reader["vmID"] + ""),
                            Int32.Parse(reader["UnitSize"] + ""),
                            decimal.Parse(reader["UnitPrice"] + ""),
                            Int32.Parse(reader["VendorID"] + ""));

                        oList[i].addItem(vi, Int32.Parse(reader["quant"] + ""));
                    }
                }

                reader.Close();
                CloseConnection();
            }

            return oList;
        }

        public static Vendor getVendor(int id)
        {
            Vendor v = new Vendor();
            string query = "SELECT * FROM Vendor WHERE VendorID=" + id.ToString();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    v.Id = Int32.Parse(reader["VendorID"] + "");
                    v.Name = reader["Name"] + "";
                    v.Email = reader["Email"] + "";
                    v.Phone = reader["Phone"] + "";
                    v.Addr = reader["Addr"] + "";
                    v.City = reader["City"] + "";
                    v.State = reader["State"] + "";
                    v.Zip = reader["Zip"] + "";
                    v.International = reader["International"] + "";
                }
                else
                {
                    v.Id = -1;
                }
                reader.Close();
                CloseConnection();
            }

            return v;
        }

        public static List<Vendor> getAllVendor()
        {
            List<Vendor> vl = new List<Vendor>();
            string query = "SELECT * FROM Vendor";

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vendor v = new Vendor
                    {
                        Id = Int32.Parse(reader["VendorID"] + ""),
                        Name = reader["Name"] + "",
                        Email = reader["Email"] + "",
                        Addr = reader["Addr"] + "",
                        City = reader["City"] + "",
                        State = reader["City"] + "",
                        Zip = reader["Zip"] + "",
                        International = reader["International"] + ""
                    };
                    vl.Add(v);
                }

                reader.Close();
                CloseConnection();
            }

            foreach(Vendor v in vl)
            {
                v.getCatalogue();
            }

            return vl;
        }

        public static Customer getCustomerById(int id)
        {
            string query = "SELECT * FROM Customer WHERE Customer.CustID=" + id.ToString();
            return getCustomer(query);
        }

        public static Customer getCustomerByEmail(string eMail)
        {
            string query = "SELECT * FROM Customer WHERE Email='" + eMail + "'";
            return getCustomer(query);
        }

        public static Customer getCustomerByPhone(string phone)
        {
            string query = "SELECT * FROM Customer WHERE Phone='" + phone + "'";
            return getCustomer(query);
        }

        private static Customer getCustomer(string q)
        {
            Customer c = new Customer();
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(q, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    c.Id = Int32.Parse(reader["CustID"] + "");
                    c.Name = reader["Name"] + "";
                    c.Email = reader["Email"] + "";
                    c.Phone = reader["Phone"] + "";
                    c.Address = reader["Addr"] + "";
                    c.City = reader["City"] + "";
                    c.State = reader["State"] + "";
                    c.Zip = reader["Zip"] + "";
                }
            reader.Close();
            CloseConnection();
            }
            return c;
        }

        public static List<Customer> getAllCustomer()
        {
            List<Customer> cust = new List<Customer>();
            string query = "SELECT * FROM Customer";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer c = new Customer
                    {
                        Id = Int32.Parse(reader["CustID"] + ""),
                        Name = reader["Name"] + "",
                        Email = reader["Email"] + "",
                        Phone = reader["Phone"] + "",
                        Address = reader["Addr"] + "",
                        City = reader["City"] + "",
                        State = reader["State"] + "",
                        Zip = reader["Zip"] + ""
                    };

                    cust.Add(c);
                }

                CloseConnection();
                reader.Close();
            }

            return cust;
        }

        public static Merchandise getMerch(int id)
        {
            Merchandise m = new Merchandise
            {
                ItemID = -1
            };
            string query = "SELECT * FROM Merch WHERE ItemID=" + id.ToString();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    m.ItemID = Int32.Parse(reader["ItemID"] + "");
                    m.Name = reader["Name"] + "";
                    m.Size = reader["Size"] + "";
                    m.Inventory = Int32.Parse(reader["Inventory"] + "");
                    m.Price = Decimal.Parse(reader["Price"] + "");
                }
                reader.Close();
                CloseConnection();
            }

            return m;
        }

        public static List<Merchandise> getMerchByVendor(int VendorID)
        {
            string query = "SELECT Merch.ItemID, Merch.Name, Merch.Size, Merch.Inventory, Merch.Price " + 
                "FROM VendorMerch RIGHT JOIN Merch " + 
                "ON VendorMerch.ItemID = Merch.ItemID " +
                "WHERE VendorMerch.VendorID = " + VendorID.ToString();

            List<Merchandise> ml = new List<Merchandise>();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Merchandise m = new Merchandise
                    {
                        ItemID = Int32.Parse(reader["ItemID"] + ""),
                        Name = reader["Name"] + "",
                        Size = reader["Size"] + "",
                        Inventory = Int32.Parse(reader["Inventory"] + ""),
                        Price = Decimal.Parse(reader["Price"] + "")
                    };
                    ml.Add(m);
                }

                reader.Close();
                CloseConnection();
            }

            return ml;
        }

        public static List<Merchandise> getAllMerch()
        {
            List<Merchandise> ml = new List<Merchandise>();
            string query = "SELECT * FROM Merch";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Merchandise m = new Merchandise
                    {
                        ItemID = Int32.Parse(reader["ItemID"] + ""),
                        Name = reader["Name"] + "",
                        Size = reader["Size"] + "",
                        Inventory = Int32.Parse(reader["Inventory"] + ""),
                        Price = Decimal.Parse(reader["Price"] + "")
                    };
                    ml.Add(m);
                }
                reader.Close();
                CloseConnection();
            }

            return ml;
        }

        public static VendorItem getVendorItem(int id)
        {
            VendorItem vi;
            int vmID = -1;
            int merchID = 0;
            int vendorID = 0;
            int uSize = 0;
            decimal uPrice = 0.0M;
            string query = "SELECT * FROM VendorMerch WHERE vmID=" + id.ToString();
            
            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    vmID = Int32.Parse(reader["vmID"] + "");
                    merchID = Int32.Parse(reader["ItemID"] + "");
                    vendorID = Int32.Parse(reader["VendorID"] + "");
                    uSize = Int32.Parse(reader["UnitSize"] + "");
                    uPrice = Decimal.Parse(reader["UnitPrice"] + "");
                }

                reader.Close();
                CloseConnection();

                vi = new VendorItem(getMerch(merchID), vendorID, uSize, uPrice, vmID);
            }
            else
            {
                vi = new VendorItem
                {
                    Vmid = vmID
                };
            }

            return vi;
        }

        public static List<OrderItem> getOrderItem(int orderID)
        {
            List<OrderItem> items = new List<OrderItem>();
            string query = "SELECT * FROM OrderItems WHERE OrderID=" + orderID.ToString();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderItem oi = new OrderItem
                    {
                        Vmid = Int32.Parse(reader["vmID"] + ""),
                        Quantity = Int32.Parse(reader["Quantity"] + ""),
                        Oid = Int32.Parse(reader["oID"] + "")
                    };
                    items.Add(oi);
                }
                reader.Close();
                CloseConnection();

                foreach(OrderItem item in items)
                {
                    item.SetVendorItem(getVendorItem(item.Vmid));
                }
            }

            return items;
        }

        public static List<TransactionItem> getTransactionItem(int Tid)
        {
            List<TransactionItem> items = new List<TransactionItem>();
            string query = "SELECT * FROM TItem WHERE TransactionID=" + Tid.ToString();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TransactionItem ti = new TransactionItem
                    {
                        ItemID = Int32.Parse(reader["ItemID"] + ""),
                        tID = Int32.Parse(reader["tID"] + ""),
                        Quantity = Int32.Parse(reader["Quantity"] + "")
                    };
                }

                reader.Close();
                CloseConnection();

                foreach (TransactionItem item in items)
                {
                    item.SetMerchandise(getMerch(item.ItemID));
                }
            }

            return items;
        }

        public static Order getOrder(int id)
        {
            Order o = new Order
            {
                OrderID = -1
            };
            string oQuery = "SELECT * FROM Orders WHERE OrderID=" + id.ToString();
            List<int> vmIDs = new List<int>();
            int vendorID = -1;
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(oQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    o.OrderID = Int32.Parse(reader["OrderID"] + "");
                    vendorID = Int32.Parse(reader["VendorID"] + "");
                    o.Placed = DateTime.Parse(reader["Placed"] + "");
                    o.Received = DateTime.Parse(reader["Received"] + "");
                    o.CalcTotal();
                }

                reader.Close();
                CloseConnection();
            }

            if (o.OrderID != -1)
            {
                o.items = getOrderItem(o.OrderID);
                if(vendorID != -1)
                {
                    o.OrderVendor = getVendor(vendorID);
                }
            }
            return o;
        }

        public static List<Transaction> getAllTransactions()
        {
            List<Transaction> trans = new List<Transaction>();
            string query = "SELECT * FROM Transactions";

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transaction t = new Transaction()
                    {
                        TransactionID = Int32.Parse(reader["TransactionID"] + ""),
                        Day = DateTime.Parse(reader["Day"] + "")
                    };
                    t.Cust.Id = Int32.Parse(reader["CustID"] + "");
                    trans.Add(t);
                }
                reader.Close();
                CloseConnection();
            }

            for(int i = 0; i < trans.Count; i++)
            {
                trans[i].Cust = getCustomerById(trans[i].Cust.Id);
                trans[i].TList = getTransactionItem(trans[i].TransactionID);
            }

            return trans;
        }

        public static List<KeyValuePair<Vendor, decimal>> getAllTransactionTotalsByVendor()
        {
            string query = "select VendorMerch.VendorID as VendorID, TItem.Quantity as Quantity, Merch.Price as Price " +
                "from ((TItem INNER JOIN Merch ON TItem.ItemID = Merch.ItemID) INNER JOIN VendorMerch ON VendorMerch.ItemID = TItem.ItemID)";
            List<decimal> totals = new List<decimal>();
            Dictionary<int, int> vendors = new Dictionary<int, int>();
            List<KeyValuePair<Vendor, decimal>> pairs = new List<KeyValuePair<Vendor, decimal>>();

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int v = Int32.Parse(reader["VendorID"] + "");
                    int quant = Int32.Parse(reader["Quantity"] + "");
                    decimal price = decimal.Parse(reader["Price"] + "");

                    if (vendors.ContainsKey(v))
                    {
                        totals[vendors[v]] += quant * price;
                    }
                    else
                    {
                        decimal tot = quant * price;
                        totals.Add(tot);
                        vendors.Add(v, totals.Count - 1);
                    }
                }

                reader.Close();
                CloseConnection();
            }

            foreach(int key in vendors.Keys)
            {
                Vendor v = getVendor(key);
                decimal tot = totals[vendors[key]];
                pairs.Add(new KeyValuePair<Vendor, decimal>(v, tot));
            }

            return pairs;
        }

        public static Transaction getTransaction(int id)
        {
            Transaction t = new Transaction
            {
                TransactionID = -1
            };
            string query = "SELECT * FROM Transactions WHERE TransactionID=" + id.ToString();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    t.TransactionID = Int32.Parse(reader["TransactionID"] + "");
                    t.Cust.Id = Int32.Parse(reader["CustID"] + "");
                }

                reader.Close();
                CloseConnection();

                t.TList = getTransactionItem(t.TransactionID);
                t.Cust = getCustomerById(t.Cust.Id);
            }

            return t;
        }

        public static List<KeyValuePair<string, decimal>> getTransactionTotalByRange(DateTime start, DateTime end)
        {
            List<decimal> totals = new List<decimal>();
            List<string> names = new List<string>();
            List<KeyValuePair<string, decimal>> kvp = new List<KeyValuePair<string, decimal>>();
            Dictionary<string, int> position = new Dictionary<string, int>();
            string query = "SELECT Vendor.Name as Name, Vendor.VendorID as vID, TItem.Quantity as Quantity, Merch.Price as Price FROM ((((" +
                "TItem RIGHT JOIN Merch on TItem.ItemID = Merch.ItemID)" +
                " INNER JOIN VendorMerch on VendorMerch.ItemID = Merch.ItemID)" +
                " INNER JOIN Vendor on Vendor.VendorID = VendorMerch.VendorID)" +
                " INNER JOIN Transactions on Transactions.TransactionID = TItem.TransactionID)" +
                " WHERE Transactions.Day>='" + start.ToString("yyyy-MM-dd") + "'" +
                " AND Transactions.Day<='" + end.ToString("yyyy-MM-dd") + "'";

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                string name = "";
                while (reader.Read())
                {
                    name = reader["Name"] + "";
                    if (position.ContainsKey(name))
                    {
                        totals[position[name]] += decimal.Parse(reader["Quantity"] + "") + decimal.Parse(reader["Price"] + "");
                    }
                    else
                    {
                        names.Add(name);
                        totals.Add(decimal.Parse(reader["Quantity"] + "") + decimal.Parse(reader["Price"] + ""));
                        position.Add(name, totals.Count - 1);
                    }
                }
                
                for(int i = 0; i < names.Count; i++)
                {
                    KeyValuePair<string, decimal> kv = new KeyValuePair<string, decimal>(names[i], totals[position[names[i]]]);
                    kvp.Add(kv);
                }

                reader.Close();
                CloseConnection();
            }

            return kvp;
        }

        public static int getLastTransactionID()
        {
            string query = "SELECT max(TransactionID) as newest FROM Transactions";
            int id = 0;

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    id = Int32.Parse(reader["newest"] + "");
                }

                reader.Close();
                CloseConnection();
            }

            return id;
        }

        public static DateTime getLastTransactionDate()
        {
            string query = "SELECT max(Day) as newest FROM Transactions";
            DateTime start = DateTime.Today;

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    start = DateTime.Parse(reader["newest"] + "");
                }

                reader.Close();
                CloseConnection();
            }

            return start;
        }

        public static DateTime getFirstTransactionDate()
        {
            string query = "SELECT min(Day) as earliest FROM Transactions;";
            DateTime start = DateTime.Today;

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    start = DateTime.Parse(reader["earliest"] + "");
                }

                reader.Close();
                CloseConnection();
            }

            return start;
        }

        public static List<Earnings> getEarningsByRange(DateTime start, DateTime end, int origin = 0)
        {
            string query;
            if(origin == 0)
            {
                query = "select * from " + TestEarningsTable + " where Day>'" + start.ToString("yyyy-MM-dd") + "' and Day<'" +
                end.ToString("yyyy-MM-dd") + "'";
            }
            else
            {
                query = "select * from " + EarningsTable + " where Day>'" + start.ToString("yyyy-MM-dd") + "' and Day<'" +
                end.ToString("yyyy-MM-dd") + "'";
            }

            List<Earnings> el = new List<Earnings>();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Earnings e = new Earnings
                    {
                        Day = DateTime.Parse(reader["Day"] + ""),
                        Cash = decimal.Parse(reader["Cash"] + ""),
                        Credit = decimal.Parse(reader["Credit"] + "")
                    };
                    el.Add(e);
                }
                reader.Close();
                CloseConnection();
            }

            return el;
        }

        public static DateTime getFirstEarningsDate(int origin = 0)
        {
            string query;
            if(origin == 0)
            {
                query = "select min(Day) as fDay from " + TestEarningsTable;
            }
            else
            {
                query = "select min(Day) as fDay from " + EarningsTable;
            }
            DateTime day = DateTime.Today;

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    day = DateTime.Parse(reader["fDay"] + "");
                }

                reader.Close();
                CloseConnection();
            }

            return day;
        }

        public static DateTime getLastEarningsDate(int origin = 0)
        {
            string query;
            if (origin == 0)
            {
                query = "select max(Day) as fDay from " + TestEarningsTable;
            }
            else
            {
                query = "select max(Day) as fDay from " + EarningsTable;
            }
            DateTime day = DateTime.Today;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    day = DateTime.Parse(reader["fDay"] + "");
                }
              
                reader.Close();
                CloseConnection();
            }

            return day;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Nexus
{
    /*
     * Summary: Handles connections to the database as well as retreival
     */
    static class DBHandler
    {
        
        private static readonly string server = "nexusgifts.c79k1z6krivv.us-east-2.rds.amazonaws.com";/**< string the url location of the database */
        private static readonly string database = "NexusDB";/**< Name of the database schema to be used */
        private static readonly string user = "Nexus"; /**< The user being logged into */
        private static readonly string pass = "Capstone2019"; /**< Password for the DB User */
        private static readonly string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + user + ";" + "PASSWORD=" + pass + ";"; /**< string to begin handshaking process with mysql */
        private static MySqlConnection connection = new MySqlConnection(connectionString); /**< variable containing the connection */

        private static readonly string EarningsTable = "DailyEarnings"; /**< Table for company's daily earnings */
        private static readonly string TestEarningsTable = "TestEarnings"; /**< Test table to be used untill full earnings is entered */

        /*
         * OpenConnection provides an open connection to the database
         */
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

        /// <summary>
        /// Closes connection to the database after processing
        /// </summary>
        /// <returns>boolean on successful close or not</returns>
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

        /// <summary>
        /// Execute a provided string that won't require a return value
        /// </summary>
        /// <param name="query">query to be executed</param>
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

        /// <summary>
        /// Executes multiple queries without any return values
        /// </summary>
        /// <param name="queries">Array of queries to be executed</param>
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

        /// <summary>
        /// Selects the ID of the most recent order
        /// </summary>
        /// <param name="VendorID">Vendor to which the order was placed</param>
        /// <param name="date">When the order was placed</param>
        /// <returns>ID of the most recent order</returns>
        public static int SelectMostRecentOrder(int VendorID, DateTime date)
        {
            String d = date.ToString("yyyy-MM-dd"); /**< string representation of the order date */
            List<int> list = new List<int>(); /**< list of integer values retrieved from the database matching the query */

            String query = "SELECT OrderID FROM Orders WHERE VendorID=" + VendorID.ToString() + " AND Placed=" + d; /**< query to find all the orders related to the vendor */

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); /**< creates the connection and issues the command*/
                MySqlDataReader reader = cmd.ExecuteReader(); /**< Executes the command, returning the result of the query*/

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

        /// <summary>
        /// Gets the total value of all the orders within the date range
        /// </summary>
        /// <param name="start">earlier date to start query</param>
        /// <param name="end">later date, end of query</param>
        /// <returns>decimal value representing the earnigns from the range</returns>
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

        /// <summary>
        /// Returns the ID of the most recent transaction ID by customer
        /// </summary>
        /// <param name="CustID">customer buying the transaction</param>
        /// <returns>ID of the most recent transaction</returns>
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

        /// <summary>
        /// Select all of the items associated with a vendor
        /// </summary>
        /// <param name="vend">vendor to retrieve</param>
        /// <returns>List of vendor Items</returns>
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

        /// <summary>
        /// the last order of them all
        /// </summary>
        /// <returns>ID of the last order</returns>
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

        /// <summary>
        /// Every order in the DB
        /// </summary>
        /// <returns>List of all orders stored</returns>
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

        /// <summary>
        /// Orders that do not have a finalized date
        /// </summary>
        /// <returns>list of orders without receival dates</returns>
        public static List<Order> getOpenOrders()
        {
            List<Order> oList = new List<Order>();
            string query = "Select * from Orders WHERE Received='0001-01-01'";

            if (OpenConnection() == true)
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

        /// <summary>
        /// Gets the orders within a range of time
        /// </summary>
        /// <param name="start">starting day of order placement</param>
        /// <param name="end">last date for order placement</param>
        /// <returns>list of returned orders</returns>
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

        /// <summary>
        /// Gets a vendor with a given ID
        /// </summary>
        /// <param name="id">the ID of the vendor</param>
        /// <returns>Filled vendor class</returns>
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

        /// <summary>
        /// Gets every vendor stored in the DB
        /// </summary>
        /// <returns>list of vendors</returns>
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

        /// <summary>
        /// gets a filled customer object by ID
        /// </summary>
        /// <param name="id">ID of the customer</param>
        /// <returns>Filled customer class</returns>
        public static Customer getCustomerById(int id)
        {
            string query = "SELECT * FROM Customer WHERE Customer.CustID=" + id.ToString();
            return getCustomer(query);
        }

        /// <summary>
        /// gets a filled customer object by Email
        /// </summary>
        /// <param name="eMail">email to query</param>
        /// <returns>Filled customer class</returns>
        public static Customer getCustomerByEmail(string eMail)
        {
            string query = "SELECT * FROM Customer WHERE Email='" + eMail + "'";
            return getCustomer(query);
        }

        /// <summary>
        /// gets a customer given a phone number
        /// </summary>
        /// <param name="phone">phone to search for</param>
        /// <returns>Filled customer class</returns>
        public static Customer getCustomerByPhone(string phone)
        {
            string query = "SELECT * FROM Customer WHERE Phone='" + phone + "'";
            return getCustomer(query);
        }

        /// <summary>
        /// uses a given string to fill in a single customer object
        /// </summary>
        /// <param name="q">provided query</param>
        /// <returns>Filled customer value</returns>
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

        /// <summary>
        /// Returns a list of all customers stored
        /// </summary>
        /// <returns>list of all customers</returns>
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

        /// <summary>
        /// Returns a filled Merchandise given an ID
        /// </summary>
        /// <param name="id">ID to query</param>
        /// <returns>Filled merchandise class</returns>
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

        /// <summary>
        /// A list of merchanise items associated with a vendor
        /// </summary>
        /// <param name="VendorID">vendor making the Merchandise</param>
        /// <returns>List of merchandise items</returns>
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

        /// <summary>
        /// Returns all the merchandise
        /// </summary>
        /// <returns>list of all merchandise</returns>
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

        /// <summary>
        /// Returns a vendor item given an ID
        /// </summary>
        /// <param name="id">ID to search for</param>
        /// <returns>filled vendoritem class</returns>
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

        /// <summary>
        /// gets the orderitems associated with an order
        /// </summary>
        /// <param name="orderID">Order to search for</param>
        /// <returns>list of items associated with an order</returns>
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

        /// <summary>
        /// Gets the transaction items associated with a transaction
        /// </summary>
        /// <param name="Tid">ID of transaction</param>
        /// <returns>List of transaction's items</returns>
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

        /// <summary>
        /// Get an order by ID
        /// </summary>
        /// <param name="id">Order ID to fulfill</param>
        /// <returns>filled Order Object</returns>
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

        /// <summary>
        /// returns every transaction
        /// </summary>
        /// <returns>list of all transactions</returns>
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

        /// <summary>
        /// Sums the total sales from each vendor's sales
        /// </summary>
        /// <returns>List of key value pairs with vendor and total</returns>
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

        /// <summary>
        /// gets a transaction given an ID
        /// </summary>
        /// <param name="id">ID of the transaction</param>
        /// <returns>Filled Transaction object</returns>
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

        /// <summary>
        /// Gets the total from transactions based on a date range
        /// </summary>
        /// <param name="start">start of date range</param>
        /// <param name="end">end of date range</param>
        /// <returns>list of key value pairs of item name and total</returns>
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

        /// <summary>
        /// Returns the ID of the last transaction
        /// </summary>
        /// <returns>ID of the last transaction</returns>
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

        /// <summary>
        /// Gets the date value of the last transaction
        /// </summary>
        /// <returns>date of the last transaction</returns>
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

        /// <summary>
        /// Gets the date of the first transaction
        /// </summary>
        /// <returns>date of the first transaction stored</returns>
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

        /// <summary>
        /// range of earnings from the db
        /// </summary>
        /// <param name="start">date to start search</param>
        /// <param name="end">date to end search</param>
        /// <param name="origin">whether to use the test values (0) or the real values</param>
        /// <returns></returns>
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

        /// <summary>
        /// gets the earliest date in earnings
        /// </summary>
        /// <param name="origin">(0) for testing table and anything else for real table values</param>
        /// <returns>date of the earliest earning</returns>
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

        /// <summary>
        /// The date of the latest earnings
        /// </summary>
        /// <param name="origin">(0) test table or otherwise for real values</param>
        /// <returns>date of latest earnings</returns>
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

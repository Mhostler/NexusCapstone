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
        private static string server = "nexusgifts.c79k1z6krivv.us-east-2.rds.amazonaws.com";
        private static string database = "NexusDB";
        private static string user = "Nexus";
        private static string pass = "Capstone2019";
        private static string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + user + ";" + "PASSWORD=" + pass + ";";
        private static MySqlConnection connection = new MySqlConnection(connectionString);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;

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
                    VendorItem v = new VendorItem();
                    v.Vmid = Int32.Parse(reader["vmID"] + "");
                    v.ItemID = Int32.Parse(reader["ItemID"] + "");
                    v.Name = reader["Name"] + "";
                    v.Size = reader["Size"] + "";
                    v.Price = Decimal.Parse(reader["Price"] + "");
                    v.Inventory = Int32.Parse(reader["Inventory"] + "");
                    v.VendorID = Int32.Parse(reader["VendorID"] + "");
                    v.UnitSize = Int32.Parse(reader["UnitSize"] + "");
                    v.UnitPrice = Decimal.Parse(reader["UnitPrice"] + "");
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

        public static Vendor getVendor(int id)
        {
            Vendor v = new Vendor();
            string query = "SELECT * FROM Vendor WHERE Vendor.VendorID=" + id.ToString();

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

            if (OpenConnection() == true)
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
                        Phone = reader["Phone"] + "",
                        Addr = reader["Addr"] + "",
                        City = reader["City"] + "",
                        State = reader["State"] + "",
                        Zip = reader["Zip"] + "",
                        International = reader["International"] + "",
                    };
                    vl.Add(v);
                }
                reader.Close();
                CloseConnection();
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
            string query = "SELECT * FROM Customer WHERE Email=" + eMail;
            return getCustomer(query);
        }

        public static Customer getCustomerByPhone(string phone)
        {
            string query = "SELECT * FROM Customer WHERE Phone=" + phone;
            return getCustomer(query);
        }

        private static Customer getCustomer(string q)
        {
            Customer c = new Customer();
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
                    Customer c = new Customer();
                    c.Id = Int32.Parse(reader["CustID"] + "");
                    c.Name = reader["Name"] + "";
                    c.Email = reader["Email"] + "";
                    c.Phone = reader["Phone"] + "";
                    c.Address = reader["Addr"] + "";
                    c.City = reader["City"] + "";
                    c.State = reader["State"] + "";
                    c.Zip = reader["Zip"] + "";

                    cust.Add(c);
                }

                CloseConnection();
            }

            return cust;
        }

        public static Merchandise getMerch(int id)
        {
            Merchandise m = new Merchandise();
            m.ItemID = -1;
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

        public static List<Merchandise> getAllMerchandise()
        {
            List<Merchandise> ml = new List<Merchandise>();
            string query = "SELECT * FROM Merch";

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
                vmID = Int32.Parse(reader["vmID"] + "");
                merchID = Int32.Parse(reader["ItemID"] + "");
                vendorID = Int32.Parse(reader["VendorID"] + "");
                uSize = Int32.Parse(reader["UnitSize"] + "");
                uPrice = Decimal.Parse(reader["UnitPrice"] + "");

                reader.Close();
                CloseConnection();

                vi = new VendorItem(getMerch(merchID), vendorID, uSize, uPrice, vmID);
            }
            else
            {
                vi = new VendorItem();
                vi.Vmid = vmID;
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
                    OrderItem oi = new OrderItem();
                    oi.Vmid = Int32.Parse(reader["vmID"] + "");
                    oi.Quantity = Int32.Parse(reader["Quantity"] + "");
                    oi.Oid = Int32.Parse(reader["oID"] + "");
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
                    TransactionItem ti = new TransactionItem();
                    ti.ItemID = Int32.Parse(reader["ItemID"] + "");
                    ti.tID = Int32.Parse(reader["tID"] + "");
                    ti.Quantity = Int32.Parse(reader["Quantity"] + "");
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
            Order o = new Order();
            o.OrderID = -1;
            string oQuery = "SELECT * FROM Orders WHERE OrderID=" + id.ToString();
            List<int> vmIDs = new List<int>();

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(oQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                o.OrderID = Int32.Parse(reader["OrderID"] + "");
                o.OrderVendor = getVendor(Int32.Parse(reader["VendorID"] + ""));
                o.items = getOrderItem(o.OrderID);
                o.CalcTotal();

                reader.Close();
                CloseConnection();
            }
            return o;
        }

        public static Transaction getTransaction(int id)
        {
            Transaction t = new Transaction();
            t.TransactionID = -1;
            string query = "SELECT * FROM Transactions WHERE TransactionID=" + id.ToString();

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                t.TransactionID = Int32.Parse(reader["TransactionID"] + "");
                t.Cust.Id = Int32.Parse(reader["CustID"] + "");

                reader.Close();
                CloseConnection();

                t.TList = getTransactionItem(t.TransactionID);
                t.Cust = getCustomerById(t.Cust.Id);
            }

            return t;
        }

        public static Earnings[] getEarningsByRange(DateTime start, DateTime end)
        {
            Earnings[] e = new Earnings[5];
            return e;
        }
    }
}

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

        //public List<String>[] Select()
        //{
        //    string query = "SELECT * FROM Merch";

        //    //Create a list to store the result
        //    List<string>[] list = new List<string>[5];
        //    list[0] = new List<string>();
        //    list[1] = new List<string>();
        //    list[2] = new List<string>();
        //    list[3] = new List<string>();
        //    list[4] = new List<string>();

        //    //Open connection
        //    if (OpenConnection() == true)
        //    {
        //        //Create Command
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        //Create a data reader and Execute the command
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {
        //            list[0].Add(dataReader["ItemID"] + "");
        //            list[1].Add(dataReader["Name"] + "");
        //            list[2].Add(dataReader["Size"] + "");
        //            list[3].Add(dataReader["Inventory"] + "");
        //            list[4].Add(dataReader["Price"] + "");
        //        }

        //        //close Data Reader
        //        dataReader.Close();

        //        //close Connection
        //        CloseConnection();

        //        //return list to be displayed
        //        return list;
        //    }
        //    else
        //    {
        //        return list;
        //    }
        //}


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Nexus
{
    class DBHandler
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string pass;

        public DBHandler()
        {
            //server = "nexusgifts.c79k1z6krivv.us-east-2.rds.amazonaws.com";
            //database = "NexusDB"
            //user = "Nexus"
            //pass = "Capstone2019"
            server = "localhost";
            database = "nexustest";
            user = "tester";
            pass = "Capstone2019";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + user + ";" + "PASSWORD=" + pass + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
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

        private bool CloseConnection()
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

        public void Insert(String name)
        {
            string query = "INSERT INTO testCon (Name) VALUES('" + name + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void InsertItem(String name, String size, int quantity, decimal price)
        {
            string query = "INSERT INTO merch (Name, Size, Quantity, Price) VALUES('" + name + "', '" +
                size + "', " + quantity.ToString() + ", " + price.ToString() + ")";

            if(this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Update()
        {

        }

        public void Delete()
        {

        }

        public List<String>[] Select()
        {
            string query = "SELECT * FROM merch";

            //Create a list to store the result
            List<string>[] list = new List<string>[5];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["MerchNo"] + "");
                    list[1].Add(dataReader["Name"] + "");
                    list[2].Add(dataReader["Size"] + "");
                    list[3].Add(dataReader["Quantity"] + "");
                    list[4].Add(dataReader["Price"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }


    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class API
    {
        public Merchandise[][] getInventory()
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create("https://nexus-gifts.myshopify.com/admin/products.json");
            // Set the credentials.
            request.Credentials = new NetworkCredential("699c6bc43f0faf199beb517d7016442c", "5ef303099c80218c94c08dee9c786667");
            // Get the response.
            HttpWebResponse response = null;
            Console.WriteLine("before connect");
            try
            {
                // This is where the HTTP GET actually occurs.
                Console.WriteLine("try to connect");
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception a)
            {
                Console.WriteLine("connect failed");
                Console.WriteLine(a.ToString());
            }
            // Display the status. You want to see "OK" here.
            Console.WriteLine(response.StatusDescription);
            Console.WriteLine("you're here");
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content. This is the XML that represents all the products for the shop.
            string responseFromServer = reader.ReadToEnd();
            // Display the content. 
            // Console.WriteLine(responseFromServer);
            // convert string to objects
            JArray json = new JArray(responseFromServer);
            var results = JsonConvert.DeserializeObject<RootObject>(responseFromServer);
            Merchandise[][] merch = results.GetMerchandise();
            /* for (int i = 0; i < merch.Length; i++)
            {
                for (int j = 0; j < merch[i].Length; j++)
                {
                    Console.WriteLine(merch[i][j].ItemID);
                }
            } */
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return merch;
        }

        public Customer[] getCustomers()
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create("https://nexus-gifts.myshopify.com/admin/customers.json");
            // Set the credentials.
            request.Credentials = new NetworkCredential("699c6bc43f0faf199beb517d7016442c", "5ef303099c80218c94c08dee9c786667");
            // Get the response.
            HttpWebResponse response = null;
            Console.WriteLine("before connect");
            try
            {
                // This is where the HTTP GET actually occurs.
                Console.WriteLine("try to connect");
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception a)
            {
                Console.WriteLine("connect failed");
                Console.WriteLine(a.ToString());
            }
            // Display the status. You want to see "OK" here.
            Console.WriteLine(response.StatusDescription);
            Console.WriteLine("you're here");
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content. This is the XML that represents all the products for the shop.
            string responseFromServer = reader.ReadToEnd();
            // Display the content. 
            // Console.WriteLine(responseFromServer);
            // convert string to objects
            JArray json = new JArray(responseFromServer);
            var results = JsonConvert.DeserializeObject<RootObject1>(responseFromServer);
            Customer[] custs = results.GetCustomers();
            Console.WriteLine("done");
            /*for (int i = 0; i < custs.Length; i++)
            {
                for (int j = 0; j < custs[i].Length; j++)
                {
                    Console.WriteLine(custs[i][j].ItemID);
                }
            } */
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return custs;
        }

        public Transaction[] getTrans()
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create("https://nexus-gifts.myshopify.com/admin/orders.json");
            // Set the credentials.
            request.Credentials = new NetworkCredential("699c6bc43f0faf199beb517d7016442c", "5ef303099c80218c94c08dee9c786667");
            // Get the response.
            HttpWebResponse response = null;
            Console.WriteLine("before connect");
            try
            {
                // This is where the HTTP GET actually occurs.
                Console.WriteLine("try to connect");
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception a)
            {
                Console.WriteLine("connect failed");
                Console.WriteLine(a.ToString());
            }
            // Display the status. You want to see "OK" here.
            Console.WriteLine(response.StatusDescription);
            Console.WriteLine("you're here");
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content. This is the XML that represents all the products for the shop.
            string responseFromServer = reader.ReadToEnd();
            // Display the content. 
            // Console.WriteLine(responseFromServer);
            // convert string to objects
            JArray json = new JArray(responseFromServer);
            var results = JsonConvert.DeserializeObject<RootObject2>(responseFromServer);
            Transaction[] trans = results.GetTransactions();
            Console.WriteLine("done");
            /*for (int i = 0; i < custs.Length; i++)
            {
                for (int j = 0; j < custs[i].Length; j++)
                {
                    Console.WriteLine(custs[i][j].ItemID);
                }
            } */
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return trans;
        }

        public Transaction[] getTrans(DateTime dateTime)
        {
            // Create a request for the URL. 
            string website = "https://nexus-gifts.myshopify.com/admin/orders.json";
            WebRequest request = WebRequest.Create(website);
            // Set the credentials.
            request.Credentials = new NetworkCredential("699c6bc43f0faf199beb517d7016442c", "5ef303099c80218c94c08dee9c786667");
            // Get the response.
            HttpWebResponse response = null;
            Console.WriteLine("before connect");
            try
            {
                // This is where the HTTP GET actually occurs.
                Console.WriteLine("try to connect");
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception a)
            {
                Console.WriteLine("connect failed");
                Console.WriteLine(a.ToString());
            }
            // Display the status. You want to see "OK" here.
            Console.WriteLine(response.StatusDescription);
            Console.WriteLine("you're here");
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content. This is the XML that represents all the products for the shop.
            string responseFromServer = reader.ReadToEnd();
            // Display the content. 
            // Console.WriteLine(responseFromServer);
            // convert string to objects
            JArray json = new JArray(responseFromServer);
            var results = JsonConvert.DeserializeObject<RootObject2>(responseFromServer);
            Transaction[] trans = results.GetTransactions();
            Console.WriteLine("done");
            /*for (int i = 0; i < custs.Length; i++)
            {
                for (int j = 0; j < custs[i].Length; j++)
                {
                    Console.WriteLine(custs[i][j].ItemID);
                }
            } */
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return trans;
        }
    }
}

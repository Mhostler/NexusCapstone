using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nexus
{
    /// <summary>
    /// Interaction logic for APITestpage.xaml
    /// </summary>
    public partial class APITestpage : Page
    {
        public APITestpage()
        {
            InitializeComponent();
        }

        // button
        private void Button_Click(object sender, RoutedEventArgs e)
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
            for (int i = 0; i < merch.Length; i++)
            {
                for (int j = 0; j < merch[i].Length; j++)
                {
                    Console.WriteLine(merch[i][j].ItemID);
                }
            }
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}

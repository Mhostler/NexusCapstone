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
        API api = new API();
        public APITestpage()
        {
            InitializeComponent();
        }

        // button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Merchandise[][] merch = api.getInventory();
            Customer[] custs = api.getCustomers();
            Transaction[] trans = api.getTrans();
        }
    }
}

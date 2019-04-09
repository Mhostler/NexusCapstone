using System;
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

namespace Nexus
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class VendorInsertPage : Page
    {
        public VendorInsertPage()
        {
            InitializeComponent();
        }
        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            Vendor v = new Vendor
            {
                Name = NameBox.Text,
                Email = EmailBox.Text,
                Phone = PhoneBox.Text,
                Addr = AddrBox.Text,
                City = CityBox.Text,
                State = StateBox.Text,
                Zip = ZipBox.Text,
                International = InternationalBox.Text
            };
            v.InsertVendor();

            PrintDialog p = new PrintDialog();
            p.Equals("Successful");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("VendorsPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);

        }
    }
}

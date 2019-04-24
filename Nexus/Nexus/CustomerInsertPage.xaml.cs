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
    public partial class CustomerInsertPage : Page
    {
        public CustomerInsertPage()
        {
            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "Insert Name" ||
                EmailBox.Text == "Email" ||
                PhoneBox.Text == "Phone" ||
                AddrBox.Text == "Address" ||
                CityBox.Text == "City" ||
                StateBox.Text == "Sate" ||
                ZipBox.Text == "Zip")
            {
                MessageBox.Show("Default Values not allowed, delete or type a new value");
            }
            else
            {
                Customer c = new Customer
                {
                    Name = NameBox.Text,
                    Email = EmailBox.Text,
                    Phone = PhoneBox.Text,
                    Address = AddrBox.Text,
                    City = CityBox.Text,
                    State = StateBox.Text,
                    Zip = ZipBox.Text
                };

                c.InsertCustomer();
                MessageBox.Show("Customer Inserted");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("CustomerPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);
        }
    }
}

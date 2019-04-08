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
           Customer c = new Customer();
            c.Name = NameBox.Text;
            c.Email = EmailBox.Text;
            c.Phone = PhoneBox.Text;
            c.Address = AddrBox.Text;
            c.City = CityBox.Text;
            c.State = StateBox.Text;
            c.Zip = ZipBox.Text;
           
            c.InsertCustomer();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("CustomerPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);
        }
    }
}

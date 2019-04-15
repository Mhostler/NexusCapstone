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
using System.Data.SqlClient;
using System.Data;

namespace Nexus
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        /*
         * Customer Information:
         * Name, Email, Phone, Street Addr, city, state, zip
         */
       

        public CustomerPage()
        {
            InitializeComponent();
           
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer();
            c.Id = Int32.Parse(IdBox.Text);
            int Ids = c.Id;
            c.Phone = PhoneBox.Text;
            String Phone = c.Phone;
            c.Email = EmailBox.Text;
            String Email = c.Email;
            var Phones = DBHandler.getCustomerByPhone(c.Phone);
            List.TryFindResource(Phones);
            var Idz = DBHandler.getCustomerById(c.Id);
            List.TryFindResource(Idz);
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer();
            List.ItemsSource = DBHandler.getAllCustomer();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("CustomerInsertPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = new DataGrid();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Customer c = new Customer();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Customer x = new Customer();
        }
    }
}

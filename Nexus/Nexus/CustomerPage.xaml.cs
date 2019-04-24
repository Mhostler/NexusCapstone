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
            if (IdBox.Text != "Enter ID"
                || PhoneBox.Text != "Enter Phone #"
                || EmailBox.Text != "Enter Email")
            {
                Customer c = new Customer();
                c.Id = Int32.Parse(IdBox.Text);
                int Ids = c.Id;
               
                List<Customer> Idz = new List<Customer>();
                Idz.Add(DBHandler.getCustomerById(Ids));
                List.ItemsSource = Idz;
                
            }
            else {
                MessageBox.Show("Default values should be changed.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            List.ItemsSource = DBHandler.getAllCustomer();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("CustomerInsertPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (IdBox.Text != "Enter ID"
                || PhoneBox.Text != "Enter Phone #"
                || EmailBox.Text != "Enter Email")
            {
                Customer c = new Customer();
                c.Phone = PhoneBox.Text;
                String Phone = c.Phone;
                List<Customer> Idz = new List<Customer>();
                Idz.Add(DBHandler.getCustomerByPhone(Phone));
                List.ItemsSource = Idz;

            }
            else
            {
                MessageBox.Show("Default values should be changed.");
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (IdBox.Text != "Enter ID"
                || PhoneBox.Text != "Enter Phone #"
                || EmailBox.Text != "Enter Email")
            {
                Customer c = new Customer();
                c.Email = EmailBox.Text;
                String Email = c.Email;
                List<Customer> Idz = new List<Customer>();
                Idz.Add(DBHandler.getCustomerByEmail(Email));
                List.ItemsSource = Idz;

            }
            else
            {
                MessageBox.Show("Default values should be changed.");
            }
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
            c.Phone = PhoneBox.Text;
            c.Email = EmailBox.Text;
            DataGrid data = new DataGrid();
            DataTable table = new DataTable();
           var db = DBHandler.getCustomer(c.Id);
            MySqlCommand cmd = new MySqlCommand("SELECT * from CUstomer where Id = ?");
            MySqlDataReader reader = cmd.ExecuteReader();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            dataAdapter.SelectCommand= cmd;
            dataAdapter.Fill(table);

                     

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer();
            var ConnectionString = ConfigurationManager.ConnectionStrings["dbHandler"].ConnectionString;
            //DataSet ds = new DataSet();
            
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("CustomerInsertPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = new DataGrid();
            DataSet ds = new DataSet();


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

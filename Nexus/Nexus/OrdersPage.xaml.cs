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
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        /*
         * Orders information
         * before any other field is selected a vendor needs to be selected
         * vendor, items, #of units ordered, price(total), date ordered, date receieved
         */
        public OrdersPage()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Ord.Text != "ID")
            {
              Order ord = new Order();
            int id = Int32.Parse(Ord.Text);
            
            
                List<Order> Idz = new List<Order>();
                Idz.Add(DBHandler.getOrder(id));
                List.ItemsSource = Idz;


            }
            else {

                MessageBox.Show ("Invalid ID. Please check the data type.");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Order or = new Order();
            or.OrderID = Int32.Parse(Ord.Text);
            int ords = or.OrderID;
            


           
        }

        private void AllOrder_Click(object sender, RoutedEventArgs e)
        {
            List<Order> oList = DBHandler.getAllOrders();
            List.ItemsSource = oList;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Uri uri = new Uri("OrderInsert.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

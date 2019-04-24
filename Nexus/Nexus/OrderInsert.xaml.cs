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
    public partial class OrderInsert : Page
    {
        List<Vendor> v = DBHandler.getAllVendor();
       
        Dictionary<int, int> us = new Dictionary<int, int>();
        Vendor vs = new Vendor();

        public OrderInsert()
        {
            InitializeComponent();
            for (int i =0; i <v.Count; i++) {
                Vend.Items.Add(v[i].Name);
                
                us.Add(i,v[i].Id);
                 }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("OrdersPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
               

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Order or = new Order();
            int mer = Merch.SelectedIndex;
            VendorItem vi = v[Vend.SelectedIndex].catalogue[mer];
            int items = Int32.Parse(Norder.Text);
            or.Placed = DateTime.Now;
            or.Received = DateTime.Now;
            or.OrderVendor = v[Vend.SelectedIndex];
            if (Vend.SelectedIndex == -1
                || Merch.SelectedIndex == -1
                || Norder.Text == "# of orders"
                || or.Placed == null)
            {
                MessageBox.Show("Boxes not selected or default values placed.");
            }
            else
            {
                or.addItem(vi, items);
                or.InsertOrder();

            }
            
            

            
        }

        private void Vend_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vendor vs = v[Vend.SelectedIndex];
            Merch.Items.Clear();
            for (int i = 0; i < vs.catalogue.Count; i++) {
                Merch.Items.Add(vs.catalogue[i].Name+" "+vs.catalogue[i].Size);

            }
        }

        private void Merch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

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
    public partial class MerchandiseInsert : Page
    {
        public MerchandiseInsert()
        {
            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != "Insert Name"
                || SizeBox.Text != "Size"
                || InventoryBox.Text != "#s of Inventory"
                || PriceBox.Text != "Price")
            {
                Merchandise m = new Merchandise
                {
                    Name = NameBox.Text,
                    Size = SizeBox.Text,
                    Inventory = Int32.Parse(InventoryBox.Text),
                    Price = Int32.Parse(PriceBox.Text)
                };
                m.InsertItem();
                MessageBox.Show("Insertion Successful");
            }
            else
            {
                MessageBox.Show("Default Values should be changed");

            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("MerchPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);
        }
    }
}

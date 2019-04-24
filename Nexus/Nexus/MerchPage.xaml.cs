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
    /// Interaction logic for MerchPage.xaml
    /// </summary>
    public partial class MerchPage : Page
    {
        /*
         * Merchandise Information
         * Name, size, price
         */
        public MerchPage()
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
            if (Idboxm.Text == "ID"
               || Idboxv.Text == "ID")
            {
                MessageBox.Show("Default Values should be changed.");

            }
            else
            {
                Vendor vend = new Vendor();

                vend.Id = Int32.Parse(Idboxv.Text);

                int ids = vend.Id;

                List.ItemsSource = DBHandler.getMerchByVendor(ids);

            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Idboxm_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
             
                Merchandise m = new Merchandise();
                m.ItemID = Int32.Parse(Idboxm.Text);
                int idds = m.ItemID;
                List<Merchandise> Idz = new List<Merchandise>();
                Idz.Add(DBHandler.getMerch(idds));
                List.ItemsSource = Idz;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("MerchandiseInsert.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}

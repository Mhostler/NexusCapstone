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

//TODO Delete this comment
namespace Nexus
{
    /// <summary>
    /// Interaction logic for VendorsPage.xaml
    /// </summary>
    public partial class VendorsPage : Page
    {
       
        /*
* Vendor Table Information
* Name (required), email, phone, street addr, city, state, zip, international(string)
*/
        public VendorsPage()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

           
       

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Vendor v = new Vendor();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vendor v = new Vendor();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Vendor V = new Vendor();
            List.ItemsSource = DBHandler.getAllVendor();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("VendorInsertPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}

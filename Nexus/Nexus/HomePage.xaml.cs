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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("SalesPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void MerchButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("MerchPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("CustomerPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void VendorButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("VendorsPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("OrdersPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}

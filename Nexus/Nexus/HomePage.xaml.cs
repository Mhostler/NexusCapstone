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
        Dictionary<int, string> months = new Dictionary<int, string>()
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };

        public Page1()
        {
            InitializeComponent();
            showColumnChart();
        }

        private void showColumnChart()
        {
            DateTime last = DBHandler.getLastEarnings(1);
            DateTime start = new DateTime(last.Year, last.Month - 2, 1);
            List<Earnings> e = DBHandler.getEarningsByRange(start, last, 1);

            List<KeyValuePair<string, decimal>> values = new List<KeyValuePair<string, decimal>>();
            decimal[] earningMonths = new decimal[12];
            for (int i = 0; i < e.Count; i++)
            {
                earningMonths[e[i].Day.Month - 1] += e[i].Total;
            }

            for (int i = 0; i < earningMonths.Length; i++)
            {
                if(earningMonths[i] > 0)
                {
                    values.Add(new KeyValuePair<string, decimal>(months[i + 1], earningMonths[i]));
                }
            }

            ColumnChart.DataContext = values;

            //List<KeyValuePair<String, double>> valueList = new List<KeyValuePair<string, double>>();
            //valueList.Add(new KeyValuePair<string, double>("January", 1122.56));
            //valueList.Add(new KeyValuePair<string, double>("February", 1073.25));
            //valueList.Add(new KeyValuePair<string, double>("March", 950.75));

            //ColumnChart.DataContext = valueList;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //String itemTest = "dbTestPage.xaml";
            String insertionTest = "DBInsertionTesting.xaml";
            Uri uri = new Uri(insertionTest, UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void APIButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("APITestpage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}

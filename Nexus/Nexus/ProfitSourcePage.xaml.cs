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
    /// Interaction logic for ProfitSourcePage.xaml
    /// </summary>
    public partial class ProfitSourcePage : Page
    {
        public ProfitSourcePage()
        {
            InitializeComponent();
            StartDatePicker.DisplayDateStart = EndDatePicker.DisplayDateStart = DBHandler.getFirstTransactionDate();
            StartDatePicker.DisplayDateEnd = EndDatePicker.DisplayDateEnd = DBHandler.getLastTransactionDate();
            StartDatePicker.DisplayDate = EndDatePicker.DisplayDate = StartDatePicker.DisplayDateStart??DateTime.Now;
            ShowChart();
        }

        public void ShowChart()
        {
            List<KeyValuePair<Vendor, decimal>> totals = DBHandler.getAllTransactionTotalsByVendor();
            List<KeyValuePair<string, decimal>> values = new List<KeyValuePair<string, decimal>>();

            for(int i = 0; i < totals.Count; i++)
            {
                values.Add(new KeyValuePair<string, decimal>(totals[i].Key.Name, totals[i].Value));
            }

            ChartSeries1.DataContext = values;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ChartingOptions.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = StartDatePicker.SelectedDate ?? StartDatePicker.DisplayDate;
            DateTime end = EndDatePicker.SelectedDate ?? EndDatePicker.DisplayDate;
            if(start > end)
            {
                MessageBox.Show("Start date must come before end date");
            }
            else
            {
                List<KeyValuePair<string, decimal>> values = DBHandler.getTransactionTotalByRange(start, end);

                ChartSeries1.DataContext = values;
            }
        }
    }
}

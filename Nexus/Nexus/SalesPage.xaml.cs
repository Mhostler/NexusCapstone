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
    /// Interaction logic for SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        public SalesPage()
        {
            InitializeComponent();
            showLineChart();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void showLineChart()
        {
            List<Earnings> el = DBHandler.getEarningsByRange(StartDatePicker.DisplayDate, EndDatePicker.DisplayDate);
            if(DMYComboBox.Text == "Year")
            {
                List<KeyValuePair<int, decimal>> values = new List<KeyValuePair<int, decimal>>();
                int i = 0;
                for(DateTime iterYear = StartDatePicker.DisplayDate; iterYear.Year <= EndDatePicker.DisplayDate.Year; iterYear.AddYears(1))
                {
                    decimal total = 0;
                    while(el[i].Day.Year == iterYear.Year)
                    {
                        total += el[i].Total;
                        i++;
                        if(i >= el.Count) { break; }
                    }
                    values.Add(new KeyValuePair<int,decimal>(iterYear.Year, total));
                }
                ProfitLineChart.DataContext = values;
            }
            else
            {
                List<KeyValuePair<DateTime, decimal>> values = new List<KeyValuePair<DateTime, decimal>>();
                for(int i = 0; i < el.Count; i++)
                    {
                        KeyValuePair<DateTime, decimal> kv = new KeyValuePair<DateTime, decimal>(el[i].Day, el[i].Total);
                        values.Add(kv);
                    }
                ProfitLineChart.DataContext = values;
            }


        }

        private void SetRangeButton_Click(object sender, RoutedEventArgs e)
        {
            showLineChart();
        }
    }
}

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
                DateTime startYear = StartDatePicker.DisplayDate;
                decimal total = 0;
                for(int i = 0; i < el.Count; i++)
                {
                    if(el[i].Day.Year == startYear.Year)
                    {
                        total += el[i].Total;
                    }
                    else
                    {
                        KeyValuePair<int, decimal> kv = new KeyValuePair<int, decimal>(startYear.Year, total);
                        values.Add(kv);
                        total = el[i].Total;
                        startYear = startYear.AddYears(1);
                    }
                }

                ProfitLineChart.DataContext = values;
            }
            else if(DMYComboBox.Text == "Month")
            {
                List<KeyValuePair<int, decimal>> values = new List<KeyValuePair<int, decimal>>();
                DateTime startMonth = StartDatePicker.DisplayDate;
                decimal total = 0;
                for (int i = 0; i < el.Count; i++)
                {
                    if (el[i].Day.Month == startMonth.Month)
                    {
                        total += el[i].Total;
                    }
                    else
                    {
                        KeyValuePair<int, decimal> kv = new KeyValuePair<int, decimal>(startMonth.Month, total);
                        values.Add(kv);
                        total = el[i].Total;
                        startMonth = startMonth.AddMonths(1);
                    }
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

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
            InitializeComparisonComboBoxes();
        }

        private void InitializeComparisonComboBoxes()
        {
            DateTime first = DBHandler.getFirstEarnings();
            for (; first.Year <= DateTime.Today.Year; first = first.AddYears(1))
            {
                FirstYearComboBox.Items.Add(first.Year);
                SecondYearComboBox.Items.Add(first.Year);
            }
            FirstYearComboBox.SelectedIndex = 0;
            SecondYearComboBox.SelectedIndex = 0;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void showLineChart()
        {
            DateTime start = StartDatePicker.SelectedDate??StartDatePicker.DisplayDate;
            DateTime end = EndDatePicker.SelectedDate??EndDatePicker.DisplayDate;
            List<Earnings> el = DBHandler.getEarningsByRange(start, end);

            if(start > end)
            {
                MessageBox.Show("Start Date must come before End Date");
            }
            else if(DMYComboBox.Text == "Year")
            {
                List<KeyValuePair<int, decimal>> values = new List<KeyValuePair<int, decimal>>();
                decimal total = 0;
                for(int i = 0; i < el.Count; i++)
                {
                    if(el[i].Day.Year == start.Year)
                    {
                        total += el[i].Total;
                    }
                    else
                    {
                        KeyValuePair<int, decimal> kv = new KeyValuePair<int, decimal>(start.Year, total);
                        values.Add(kv);
                        total = el[i].Total;
                        start = start.AddYears(1);
                    }
                }
                KeyValuePair<int, decimal> kvlast = new KeyValuePair<int, decimal>(start.Year, total);
                values.Add(kvlast);

                List<List<KeyValuePair<int, decimal>>> v = new List<List<KeyValuePair<int, decimal>>>
                {
                    values
                };
                ProfitLineChart.DataContext = v;
            }
            else if(DMYComboBox.Text == "Month")
            {
                List<KeyValuePair<int, decimal>> values = new List<KeyValuePair<int, decimal>>();
                decimal total = 0;
                for (int i = 0; i < el.Count; i++)
                {
                    if (el[i].Day.Month == start.Month)
                    {
                        total += el[i].Total;
                    }
                    else
                    {
                        KeyValuePair<int, decimal> kv = new KeyValuePair<int, decimal>(start.Month, total);
                        values.Add(kv);
                        total = el[i].Total;
                        start = start.AddMonths(1);
                    }
                }
                KeyValuePair<int, decimal> kvlast = new KeyValuePair<int, decimal>(start.Month, total);
                values.Add(kvlast);

                List<List<KeyValuePair<int, decimal>>> v = new List<List<KeyValuePair<int, decimal>>>
                {
                    values
                };
                ProfitLineChart.DataContext = v;
            }
            else
            {
                List<KeyValuePair<int, decimal>> values = new List<KeyValuePair<int, decimal>>();
                for(int i = 0; i < el.Count; i++)
                    {
                        KeyValuePair<int, decimal> kv = new KeyValuePair<int, decimal>(i+1, el[i].Total);
                        values.Add(kv);
                    }
                List<List<KeyValuePair<int, decimal>>> v = new List<List<KeyValuePair<int, decimal>>>
                {
                    values
                };
                ProfitLineChart.DataContext = v;
            }
        }

        private void SetRangeButton_Click(object sender, RoutedEventArgs e)
        {
            showLineChart();
        }

        private void CompareDateButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CompareButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstMonthComboBox.SelectedIndex == 0 &&
                SecondMonthComboBox.SelectedIndex == 0)
            {
                //get the range of dates to ascribe values
                int firstYear = Int32.Parse(FirstYearComboBox.SelectedItem.ToString());
                int secondYear = Int32.Parse(SecondYearComboBox.SelectedItem.ToString());
                DateTime first = new DateTime(firstYear, 1, 1);
                DateTime second = new DateTime(secondYear, 1, 1);

                //fill out lists of earnings
                List<Earnings> e1 = DBHandler.getEarningsByRange(first, first.AddYears(1));
                List<Earnings> e2 = DBHandler.getEarningsByRange(second, second.AddYears(1));

                //get totals for months and into lists for display
                List<KeyValuePair<int, decimal>> values1 = new List<KeyValuePair<int, decimal>>();
                List<KeyValuePair<int, decimal>> values2 = new List<KeyValuePair<int, decimal>>();
                decimal total = 0;
                DateTime start = new DateTime(firstYear, 1, 1);
                for(int i = 0; i < e1.Count; i++)
                {
                    if(e1[i].Day.Month == start.Month)
                    {
                        total += e1[i].Total;
                    }
                    else
                    {
                        values1.Add(new KeyValuePair<int, decimal>(start.Month, total));
                        total = e1[i].Total;
                        start = start.AddMonths(1);
                    }
                }
                values1.Add(new KeyValuePair<int, decimal>(start.Month, total));
                start = new DateTime(secondYear, 1, 1);
                total = 0;

                for(int i = 0; i < e2.Count; i++)
                {
                    if(e2[i].Day.Month == start.Month)
                    {
                        total += e2[i].Total;
                    }
                    else
                    {
                        values2.Add(new KeyValuePair<int, decimal>(start.Month, total));
                        total = e2[i].Total;
                        start = start.AddMonths(1);
                    }
                }
                values2.Add(new KeyValuePair<int, decimal>(start.Month, total));

                List<List<KeyValuePair<int, decimal>>> values = new List<List<KeyValuePair<int, decimal>>>()
                {
                    values1,
                    values2
                };
                ProfitLineChart.DataContext = values;
                ProfitLineChart2.DataContext = values;
            }
            else if(FirstMonthComboBox.SelectedIndex != 0 &&
                SecondMonthComboBox.SelectedIndex != 0)
            {
                //get the range of dates to ascribe values
                int firstYear = Int32.Parse(FirstYearComboBox.SelectedItem.ToString());
                int firstMonth = FirstMonthComboBox.SelectedIndex;
                int secondYear = Int32.Parse(SecondYearComboBox.SelectedItem.ToString());
                int secondMonth = SecondMonthComboBox.SelectedIndex;
                DateTime first = new DateTime(firstYear, firstMonth, 1);
                DateTime second = new DateTime(secondYear, secondMonth, 1);

                //fill out the lists of earnings
                List<List<Earnings>> el = new List<List<Earnings>>
                {
                    DBHandler.getEarningsByRange(first, first.AddMonths(1)),
                    DBHandler.getEarningsByRange(second, second.AddMonths(1))
                };
                List<List<KeyValuePair<int, decimal>>> values = new List<List<KeyValuePair<int, decimal>>>
                {
                    new List<KeyValuePair<int, decimal>>(),
                    new List<KeyValuePair<int, decimal>>()
                };
                for (int i = 0; i < el[0].Count; i++)
                {
                    values[0].Add(new KeyValuePair<int, decimal>(el[0][i].Day.Day, el[0][i].Total));
                }
                for (int i = 0; i < el[1].Count; i++)
                {
                    values[1].Add(new KeyValuePair<int, decimal>(el[1][i].Day.Day, el[1][i].Total));
                }
                ProfitLineChart.DataContext = values;
                ProfitLineChart2.DataContext = values;
            }
            else
            {
                MessageBox.Show("Compare format incorrect, please reexamine selection.");
            }

        }
    }
}

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
    /// Interaction logic for CostandProfit.xaml
    /// </summary>
    public partial class CostandProfit : Page
    {
        public CostandProfit()
        {
            InitializeComponent();

            GroupingComboBox.Items.Add("Day");
            GroupingComboBox.Items.Add("Month");
            GroupingComboBox.Items.Add("Year");

            for(DateTime start = new DateTime(2016, 1, 1); start.Year <= DateTime.Now.Year; start = start.AddYears(1))
            {
                YearComboBox.Items.Add(start.Year.ToString());
            }

            MonthComboBox.Items.Add("January");
            MonthComboBox.Items.Add("February");
            MonthComboBox.Items.Add("March");
            MonthComboBox.Items.Add("April");
            MonthComboBox.Items.Add("May");
            MonthComboBox.Items.Add("June");
            MonthComboBox.Items.Add("July");
            MonthComboBox.Items.Add("August");
            MonthComboBox.Items.Add("September");
            MonthComboBox.Items.Add("October");
            MonthComboBox.Items.Add("November");
            MonthComboBox.Items.Add("December");
        }

        public void ShowChart()
        {

        }

        private void FillChart()
        {
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ChartingOptions.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void GroupingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string group = GroupingComboBox.SelectedItem.ToString();
            if(group == "Day")
            {
                DayComboBox.IsEnabled = true;
                MonthComboBox.IsEnabled = true;
                YearComboBox.IsEnabled = true;
            }
            else if(group == "Month")
            {
                DayComboBox.IsEnabled = false;
                MonthComboBox.IsEnabled = true;
                YearComboBox.IsEnabled = true;
            }
            else
            {
                DayComboBox.IsEnabled = false;
                MonthComboBox.IsEnabled = false;
                YearComboBox.IsEnabled = true;
            }
        }

        private void MonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime m = new DateTime(Int32.Parse(YearComboBox.SelectedItem.ToString()),
                MonthComboBox.SelectedIndex + 1, 1);

            DayComboBox.Items.Clear();
            for(DateTime end = m.AddMonths(1); m < end; m = m.AddDays(1))
            {
                DayComboBox.Items.Add(m.Day.ToString());
            }
        }

        private void ChartingButton_Click(object sender, RoutedEventArgs e)
        {
            string group = GroupingComboBox.SelectedValue.ToString();

            if(group == "Day")
            {
                int day = -1;
                int month = -1;
                int year = -1;

                if(YearComboBox.SelectedItem != null)
                {
                    Int32.TryParse(YearComboBox.SelectedItem.ToString(), out year);
                }
                else
                {
                    year = -1;
                }

                month = MonthComboBox.SelectedIndex;

                if (MonthComboBox.SelectedItem != null)
                {
                    Int32.TryParse(DayComboBox.SelectedItem.ToString(), out day);
                }
                else
                {
                    day = -1;
                }

                if(day != -1 && month != -1 && year != -1)
                {
                    DateTime date = new DateTime(year, month, day);
                    DateTime before = date.AddDays(-1);
                    DateTime after = date.AddDays(1);
                    decimal cost = DBHandler.getOrderTotalByRange(before, after);
                    decimal income = 0;
                    List<Earnings> earnings = DBHandler.getEarningsByRange(before, after);
                    List<KeyValuePair<string, decimal>> trans = DBHandler.getTransactionTotalByRange(before, after);

                    foreach( Earnings earn in earnings)
                    {
                        income += earn.Total;
                    }

                    foreach( KeyValuePair<string, decimal> pair in trans)
                    {
                        income += pair.Value;
                    }

                    KeyValuePair<string, decimal> one = new KeyValuePair<string, decimal>("Income", income);
                    KeyValuePair<string, decimal> two = new KeyValuePair<string, decimal>("Cost", cost);
                    List<KeyValuePair<string, decimal>> list = new List<KeyValuePair<string, decimal>>()
                    {
                        one,
                        two
                    };

                    ColumnSeries1.DataContext = list;
                }
                else
                {
                    MessageBox.Show("Error with selection please look over entries");
                }
            }
            else if (group == "Month")
            {
                int year = -1;
                int month = MonthComboBox.SelectedIndex;

                if(YearComboBox.SelectedItem != null)
                {
                    Int32.TryParse(YearComboBox.SelectedItem.ToString(), out year);
                }
                else
                {
                    year = -1;
                }

                if(year != -1 || month != -1)
                {
                    DateTime start = new DateTime(year, month, 1);
                    DateTime end = start.AddMonths(1);

                    decimal cost = DBHandler.getOrderTotalByRange(start, end);
                    decimal income = 0;
                    List<Earnings> earnings = DBHandler.getEarningsByRange(start, end);
                    List<KeyValuePair<string, decimal>> trans = DBHandler.getTransactionTotalByRange(start, end);

                    foreach (Earnings earn in earnings)
                    {
                        income += earn.Total;
                    }

                    foreach (KeyValuePair<string, decimal> pair in trans)
                    {
                        income += pair.Value;
                    }

                    KeyValuePair<string, decimal> one = new KeyValuePair<string, decimal>("Income", income);
                    KeyValuePair<string, decimal> two = new KeyValuePair<string, decimal>("Cost", cost);
                    List<KeyValuePair<string, decimal>> list = new List<KeyValuePair<string, decimal>>()
                    {
                        one,
                        two
                    };

                    ColumnSeries1.DataContext = list;
                }
                else
                {
                    MessageBox.Show("Error with selection please look over entries");
                }
            }
            else if (group == "Year")
            {
                int year = -1;
                
                if(YearComboBox.SelectedItem != null)
                {
                    Int32.TryParse(YearComboBox.SelectedItem.ToString(), out year);
                }
                else
                {
                    year = -1;
                }

                if(year != -1)
                {
                    DateTime start = new DateTime(year, 1, 1);
                    DateTime end = start.AddYears(1);
                    decimal cost = DBHandler.getOrderTotalByRange(start, end);
                    decimal income = 0;
                    List<Earnings> earnings = DBHandler.getEarningsByRange(start, end);
                    List<KeyValuePair<string, decimal>> trans = DBHandler.getTransactionTotalByRange(start, end);

                    foreach (Earnings earn in earnings)
                    {
                        income += earn.Total;
                    }

                    foreach (KeyValuePair<string, decimal> pair in trans)
                    {
                        income += pair.Value;
                    }

                    KeyValuePair<string, decimal> one = new KeyValuePair<string, decimal>("Income", income);
                    KeyValuePair<string, decimal> two = new KeyValuePair<string, decimal>("Cost", cost);
                    List<KeyValuePair<string, decimal>> list = new List<KeyValuePair<string, decimal>>()
                        {
                            one,
                            two
                        };

                    ColumnSeries1.DataContext = list;
                }
                else
                {
                    MessageBox.Show("Error with selection please look over entries");
                }

            }
            else
            {
                MessageBox.Show("Error with selection please look over entries");
            }
        }
    }
}

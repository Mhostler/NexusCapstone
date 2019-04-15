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
    /// Interaction logic for DBInsertionTesting.xaml
    /// </summary>
    public partial class DBInsertionTesting : Page
    {
        public DBInsertionTesting()
        {
            InitializeComponent();
        }

        private void InsertEarningsButton_Click(object sender, RoutedEventArgs e)
        {
            Earnings earn = new Earnings
            {
                Day = EarningsDatePicker.DisplayDate,
                Cash = CashCurrencyBox.Number,
                Credit = CreditCurrencyBox.Number
            };
            earn.Insert();
        }

        private void UpdateEarningsButton_Click(object sender, RoutedEventArgs e)
        {
            Earnings earn = new Earnings
            {
                Id = Int32.Parse(EarningsIdTextBox.Text),
                Day = EarningsDatePicker.DisplayDate,
                Cash = CashCurrencyBox.Number,
                Credit = CreditCurrencyBox.Number
            };
            earn.Update();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void CreateRandomEarningsButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = new DateTime(2016, 1, 1);
            int end_year = 2019;
            List<string> queries = new List<string>();
            Random r = new Random();
            for( ; start.Year < end_year; start = start.AddDays(1))
            {
                decimal cash = r.Next(1000) + (decimal)r.Next(100) * 0.01M;
                decimal credit = r.Next(1000) + (decimal)r.Next(100) * 0.01M;
                queries.Add("INSERT INTO TestEarnings (Day, Cash, Credit) VALUES ('" + start.ToString("yyyy-MM-dd") + "', " + cash.ToString() +
                    ", " + credit.ToString() + ")");
            }

            DBHandler.ExecuteMultipleNoReturn(queries.ToArray());
        }
    }
}

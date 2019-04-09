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
    }
}

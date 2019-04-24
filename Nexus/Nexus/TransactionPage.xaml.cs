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
    /// Interaction logic for TransactionPage.xaml
    /// </summary>
    public partial class TransactionPage : Page
    {
        API api = new API();
        public TransactionPage()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri); // return home
        }

        private void getTransButton_Click(object sender, RoutedEventArgs e)
        {
            // get new transactions
            Transaction[] trans = api.getTrans(DBHandler.getLastTransactionDate());

            if(trans.Length > 0) // if there are new transactions
            {
                TransactionScrollViewer.Content = "New Transaction Dates:";
                if (trans != null)
                {
                    for (int i = 0; i < trans.Length; i++)
                    {
                        // display transaction
                        TransactionScrollViewer.Content += "\n  " + trans[i].Day.ToString("yyyy-MM-dd");
                    }
                }
            }
            else // if there aren't new transactions
            {
                TransactionScrollViewer.Content = "No New Transactions";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for dbTestPage.xaml
    /// </summary>
    public partial class dbTestPage : Page
    {
        DBHandler db = new DBHandler();

        public dbTestPage()
        {
            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            String name = NameBox.Text;
            String size = SizeTextBox.Text;
            int quantity = Convert.ToInt32(QuantityTextBox.Text);
            decimal price = PriceBox.Number;
            db.InsertItem(name, size, quantity, price);
        }

        private void RetrieveNameButton_Click(object sender, RoutedEventArgs e)
        {
            List<String> [] result = db.Select();
            String ind, name, size, quantity, price;
            String text = "";
            for (int i = 0; i < result[0].Count; i++)
            {
                ind = result[0][i];
                name = result[1][i];
                size = result[2][i];
                quantity = result[3][i];
                price = result[4][i];
                text += ind + " " + name + " " + size + " " + quantity + " " +
                    price + "\n";
            }
            NameRetrievalText.Text = text;
        }

        private void numOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = numCheck(e.Text);
        }

        private bool numCheck(String str)
        {
            Regex r = new Regex(@"[^0-9]");
            return r.IsMatch(str);
        }
    }
}

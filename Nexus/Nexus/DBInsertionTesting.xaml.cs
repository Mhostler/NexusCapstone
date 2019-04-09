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

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            Vendor v = new Vendor
            {
                Name = NameBox.Text,
                Email = EmailBox.Text,
                Phone = PhoneBox.Text,
                Addr = AddrBox.Text,
                City = CityBox.Text,
                State = StateBox.Text,
                Zip = ZipBox.Text,
                International = InternationalBox.Text
            };
            v.InsertVendor();
        }

        private void VIInsertButton_Click(object sender, RoutedEventArgs e)
        {
            VendorItem vi = new VendorItem
            {
                ItemID = Int32.Parse(ItemIDBox.Text),
                VendorID = Int32.Parse(VendorIDBox.Text),
                UnitSize = Int32.Parse(UnitSizeBox.Text),
                UnitPrice = Int32.Parse(UnitPriceBox.Text)
            };
            vi.InsertVendorItem();
        }

        private void VendorCatalogueButton_Click(object sender, RoutedEventArgs e)
        {
            Vendor v = new Vendor
            {
                Id = 1,
                Name = "Test Vendor",
                Email = "Test@Testing.tst",
                Phone = "1(402)727-7272",
                Addr = "1234 Somewhere ave.",
                City = "Omaha",
                State = "Nebraska",
                Zip = "68046",
                International = "None"
            };

            v.getCatalogue();
            String msg = v.catalogue.First().Name;
            MessageBox.Show(msg);
        }

        private void Insert_Earnings_Click(object sender, RoutedEventArgs e)
        {
            DateTime d = EarningDate.SelectedDate??DateTime.Now;
            string dFormat = "yyyy-MM-dd";
            string cash = CashEarningBox.Text;
            string credit = CreditEarningBox.Text;

            string query = "INSERT INTO DailyEarnings (Day, Cash, Credit) VALUES ('" +
                d.ToString(dFormat) + "', " + cash + ", " + credit + ")";

            DBHandler.ExecuteNoReturn(query);
        }

        private void ItemInsertButton_Click(object sender, RoutedEventArgs e)
        {
            Merchandise m = new Merchandise
            {
                Name = ItemNameTextBox.Text,
                Size = ItemSizeTextBox.Text,
                Inventory = Int32.Parse(ItemInventoryTextBox.Text),
                Price = Decimal.Parse(ItemPriceTextBox.Text)
            };
            m.InsertItem();
        }

        private void PageMoveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World");
        }
    }
}

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
            Vendor v = new Vendor();
            v.Name = NameBox.Text;
            v.Email = EmailBox.Text;
            v.Phone = PhoneBox.Text;
            v.Addr = AddrBox.Text;
            v.City = CityBox.Text;
            v.State = StateBox.Text;
            v.Zip = ZipBox.Text;
            v.International = InternationalBox.Text;
            v.InsertVendor();
        }

        private void VIInsertButton_Click(object sender, RoutedEventArgs e)
        {
            VendorItem vi = new VendorItem();
            vi.ItemID = Int32.Parse(ItemIDBox.Text);
            vi.VendorID = Int32.Parse(VendorIDBox.Text);
            vi.UnitSize = Int32.Parse(UnitSizeBox.Text);
            vi.UnitPrice = Int32.Parse(UnitPriceBox.Text);
            vi.InsertVendorItem();
        }

        private void VendorCatalogueButton_Click(object sender, RoutedEventArgs e)
        {
            Vendor v = new Vendor();
            v.Id = 1;
            v.Name = "Test Vendor";
            v.Email = "Test@Testing.tst";
            v.Phone = "1(402)727-7272";
            v.Addr = "1234 Somewhere ave.";
            v.City = "Omaha";
            v.State = "Nebraska";
            v.Zip = "68046";
            v.International = "None";

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
            Merchandise m = new Merchandise();
            m.Name = ItemNameTextBox.Text;
            m.Size = ItemSizeTextBox.Text;
            m.Inventory = Int32.Parse(ItemInventoryTextBox.Text);
            m.Price = Decimal.Parse(ItemPriceTextBox.Text);
            m.InsertItem();
        }

        private void PageMoveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World");
        }
    }
}

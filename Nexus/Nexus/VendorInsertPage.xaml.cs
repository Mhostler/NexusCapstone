﻿using System;
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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class VendorInsertPage : Page
    {
        public VendorInsertPage()
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

            MessageBox.Show("Insert Successful");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Uri u = new Uri("VendorsPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(u);

        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vendor v = new Vendor();
        }
    }
}

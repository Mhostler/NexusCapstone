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
    /// Interaction logic for ChartingOptions.xaml
    /// </summary>
    public partial class ChartingOptions : Page
    {
        public ChartingOptions()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("HomePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void SalesChartButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("SalesPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void VendorChartButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ProfitSourcePage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void ProfitCostButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("CostandProfit.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}

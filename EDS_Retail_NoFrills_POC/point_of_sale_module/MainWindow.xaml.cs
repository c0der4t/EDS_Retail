using point_of_sale_module.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace point_of_sale_module
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SaleLineItem> _activeSale;

        public MainWindow()
        {
            InitializeComponent();

            _activeSale = new List<SaleLineItem>();
            dbgActiveSaleInfo.ItemsSource = _activeSale;

            NewSale();
        }

        private void NewSale()
        {
            _activeSale.Clear();
            dbgActiveSaleInfo.AutoGenerateColumns = true;
            dbgActiveSaleInfo.Columns.Clear();
            NewLineItem();
        }

        private void NewLineItem()
        {
            edtQtyNumber.Text = "1";
            edtSKU.Clear();
            edtPrice.Clear();
            edtProductDescr.Clear();
            edtSKU.Focus();
        }


        private void edtSKU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var currLineItem = new SaleLineItem();
                currLineItem.addToSale(edtSKU.Text, edtQtyNumber.Text, edtPrice.Text, edtProductDescr.Text);

                _activeSale.Add(currLineItem);

                dbgActiveSaleInfo.ItemsSource = null;
                dbgActiveSaleInfo.ItemsSource = _activeSale;

                NewLineItem();
            }



        }

        private void btnQtyQuickButton1_Click(object sender, RoutedEventArgs e)
        {
            edtQtyNumber.Text = btnQtyQuickButton1.Content.ToString();
        }

        private void btnQtyQuickButton2_Click(object sender, RoutedEventArgs e)
        {
            edtQtyNumber.Text = btnQtyQuickButton2.Content.ToString();
        }

        private void btnQtyQuickButton3_Click(object sender, RoutedEventArgs e)
        {
            edtQtyNumber.Text = btnQtyQuickButton3.Content.ToString();
        }

        private void btnChangePrice_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The price may not be changed at this time");
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            NewSale();
        }
    }
}

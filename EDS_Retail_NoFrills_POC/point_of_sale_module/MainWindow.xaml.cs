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
        ActiveSale vActiveSale;

        public MainWindow()
        {
            InitializeComponent();
            vActiveSale = new ActiveSale();
        }

        private void NewSale()
        {

            dbgActiveSaleInfo.AutoGenerateColumns = true;
            dbgActiveSaleInfo.Columns.Clear();
            NewSKU();
        }

        private void NewSKU()
        {
            edtQtyNumber.Text = "1";
            edtSKU.Text = String.Empty;
            edtPrice.Text = String.Empty;
            redtProductDescr.Document.Blocks.Clear();
            edtSKU.Focus();
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

        private void edtSKU_KeyDown(object sender, KeyEventArgs e)
        {
            var NewLineItem = new SaleLineItem();
            NewLineItem.addToSale(edtSKU.Text, edtQtyNumber.Text, edtPrice.Text, redtProductDescr.Document.ToString());

            vActiveSale.LineItem = NewLineItem;

            NewSKU();
        }
    }
}

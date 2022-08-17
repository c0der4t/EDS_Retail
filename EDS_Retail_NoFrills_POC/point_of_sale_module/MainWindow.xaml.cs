using point_of_sale_module.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.IO;
using System.Windows.Input;

namespace point_of_sale_module
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SaleLineItem> _activeSale;
        private double _activeSaleTotal;

        public MainWindow()
        {
            InitializeComponent();

            _activeSale = new List<SaleLineItem>();
            dbgActiveSaleInfo.ItemsSource = _activeSale;

            NewSale();
        }

        private void NewSale()
        {
            _activeSaleTotal = 0;
            _activeSale.Clear();
            dbgActiveSaleInfo.AutoGenerateColumns = true;
            dbgActiveSaleInfo.Columns.Clear();
            UpdateActiveSaleTotal("0");
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

        private void UpdateActiveSaleTotal(string _lineItemPrice)
        {
            // #ToFix
            _lineItemPrice = _lineItemPrice.Replace('.', ',');

            double _convertedLineItemPrice = 0;
            try
            {
                _convertedLineItemPrice = Convert.ToDouble(_lineItemPrice);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An exception occured while updating the sale total:\n{e.ToString()}");
                throw;
            }

            _activeSaleTotal += _convertedLineItemPrice;

            lblCurrentSaleTotal.Text = _activeSaleTotal.ToString();
        }

        private void edtSKU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                //Save to a temp file
                using (StreamWriter _tempSalesFile = new StreamWriter(@"C:\temp\sale000.txt", append: true))
                {
                    _tempSalesFile.WriteLine($"{edtSKU.Text}[9-9]{edtQtyNumber.Text}[9-9]{edtPrice.Text}[9-9]{edtProductDescr.Text}");
                }

                var currLineItem = new SaleLineItem();
                currLineItem.addToSale(edtSKU.Text, edtQtyNumber.Text, edtPrice.Text, edtProductDescr.Text);

                _activeSale.Add(currLineItem);

                dbgActiveSaleInfo.ItemsSource = null;
                dbgActiveSaleInfo.ItemsSource = _activeSale;

                UpdateActiveSaleTotal(edtPrice.Text);

                NewLineItem();
            }



        }

        private void RecoverSale()
        {
            if (File.Exists(@"C:\temp\sale000.txt") == true)
            {
               var recoverDlgResult =  MessageBox.Show("A failed sale has been found, do you want to recover it?",
                    "Recover sale", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (recoverDlgResult == MessageBoxResult.Yes)
                {
                    //Load file
                    string currLine = "";

                    using (StreamReader _recoveredSaleFile = new StreamReader(@"C:\temp\sale000.txt"))
                    {
                       
                        while ((currLine = _recoveredSaleFile.ReadLine()) != null)
                        {

                        }
                    }

                    // #Twitch
                    //Parse file
                    //Load items into sale
                    //Update live sale total
                }
            }
        }


        #region UI Events

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
        #endregion

    }
}

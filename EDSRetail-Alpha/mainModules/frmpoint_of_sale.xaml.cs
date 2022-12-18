using mainModules.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for point_of_sale.xaml
    /// </summary>
    public partial class point_of_sale : Window
    {
        private double _activeSaleTotal;

        private List<databaseAPI.Models.Sale> _activeSale;
        string CurrentSaleID;
        private SalesContext _contextSales =
        new SalesContext();

        public point_of_sale()
        {
            InitializeComponent();
            InitDB();

            dbgActiveSaleInfo.ItemsSource = _activeSale;

            NewSale();
        }

        private void NewSale()
        {
            _activeSaleTotal = 0;
            _activeSale.Clear();
            dbgActiveSaleInfo.AutoGenerateColumns = true;
            dbgActiveSaleInfo.Columns.Clear();

            CurrentSaleID = databaseAPI.utilities.RandomUniqueID();

            UpdateActiveSaleTotal(0);
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

        /// <summary>
        /// Adds given double to running sale total for the current sale
        /// </summary>
        /// <param name="_lineItemPrice">Price of the line item total. If QTY > 1 , then lineitemPrice == sellprice * qty</param>
        private void UpdateActiveSaleTotal(double _lineItemPrice)
        {
            _activeSaleTotal += _lineItemPrice;

            lblCurrentSaleTotal.Text = _activeSaleTotal.ToString();
        }

        /// <summary>
        /// Builds object of Sale based off parameters. Adds to live receipt. Calls UpdateActiveSaleTotal.
        /// </summary>
        /// <param name="_saleID">Unique ID of the current / active sale</param>
        /// <param name="_sku">SKU of the item just scanned / added to the sale</param>
        /// <param name="_qty">QTY of given SKU added to the sale</param>
        /// <param name="_price">Price for 1 unit of a given SKU. NOT sellprice * qty</param>
        /// <param name="_descr">Description of given SKU as at time of scanning</param>
        private void AddLinetoActiveSale(string _saleID, string _sku, double _qty, double _price, string _descr)
        {
            var currLineItem = new databaseAPI.Models.Sale();


            currLineItem.SaleID = _saleID;
            currLineItem.SaleIDHASH = GetHashFromString(_saleID);
            currLineItem.SKU = _sku;
            currLineItem.QTY = _qty;
            currLineItem.Price = _price;
            currLineItem.Description = _descr;

            _activeSale.Add(currLineItem);
            dbgActiveSaleInfo.ItemsSource = null;
            dbgActiveSaleInfo.ItemsSource = _activeSale;

            UpdateActiveSaleTotal(_price * _qty);
        }


        /// <summary>
        /// Returns SHA256 hash of a given string.
        /// </summary>
        /// <param name="_unhashedString">String you'd like to hash using SHA256</param>
        /// <returns></returns>
        public static string GetHashFromString(string _unhashedString)
        {
            byte[] HashByteArray;

            using (HashAlgorithm algorithm = SHA256.Create())
                HashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(_unhashedString));


            StringBuilder sb = new StringBuilder();
            foreach (byte b in HashByteArray)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }


        #region UI Events

        private void edtSKU_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                try
                {
                    
                    using (var _localcontextStock = new StockContext())
                    {
                        var stockItem = _localcontextStock.Stock
                            .Single(x => x.SKU == edtSKU.Text);

                        AddLinetoActiveSale(CurrentSaleID, stockItem.SKU, Convert.ToDouble(edtQtyNumber.Text), stockItem.SellPrice, stockItem.Description);
                    }

                    NewLineItem();

                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show($"SKU not found.\n{edtSKU.Text}");
                }
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
            PostSaletoDB(_activeSale);
            NewSale();
        }

        private void btnVoidSale_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult VoidSale = MessageBox.Show("Are you sure you'd like to void this sale.",
                "Confirm Void", MessageBoxButton.YesNo);

            if (VoidSale == MessageBoxResult.Yes)
            {
                NewSale();
            }

        }
        #endregion

        #region DB Interaction

        private void InitDB()
        {
            _activeSale = new List<databaseAPI.Models.Sale>();
            CurrentSaleID = null;
        }


        /// <summary>
        /// Posts a list of Sale objects to the sales table AS IS
        /// </summary>
        /// <param name="SaleInfo">A list of type databaseAPI.Models.Sale</param>
        private void PostSaletoDB(List<databaseAPI.Models.Sale> SaleInfo)
        {
            //Open the DB / SalesContext to write to. Using auto disposes
            using (var _localcontextSales = new SalesContext())
            {
                foreach (databaseAPI.Models.Sale SingleSaleItem in SaleInfo)
                {
                    //Table is based on same databaseAPI.Models.Sale model. So we pass it the
                    //object from the list directly and save the item
                    _localcontextSales.Sales.Add(SingleSaleItem);
                }

                _localcontextSales.SaveChanges();
            }
        }




        #endregion

        private void btnVoidLine_Click(object sender, RoutedEventArgs e)
        {
            //Get the selected DBG line
            //Grab line #
            //Confirm line # void
            //Remove the line number from active sale

            int LineNum = dbgActiveSaleInfo.SelectedIndex;

            MessageBoxResult VoidLine = MessageBox.Show("Are you sure you'd like to void the selected line?",
               "Confirm Line Void", MessageBoxButton.YesNo);

            if (VoidLine == MessageBoxResult.Yes)
            {
                double voidPrice = _activeSale[LineNum].Price;
                double voidQTY = _activeSale[LineNum].QTY;

                _activeSale.RemoveAt(LineNum);
                dbgActiveSaleInfo.ItemsSource = null;
                dbgActiveSaleInfo.ItemsSource = _activeSale;

                UpdateActiveSaleTotal(-(voidPrice * voidQTY));

                NewLineItem();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            //ToDo : Show Login Window on logout click
        }
    }
}

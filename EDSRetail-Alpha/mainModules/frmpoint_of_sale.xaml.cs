using mainModules.Models;
using databaseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for point_of_sale.xaml
    /// </summary>
    public partial class point_of_sale : Window
    {

        private double _activeSaleTotal;
        private static string _flatFileDelimeter = "#";

        private List<databaseAPI.Models.Sale> _activeSale;
        private SalesContext _contextSales =
       new SalesContext();

        public point_of_sale()
        {
            InitializeComponent();
            InitDB();

            dbgActiveSaleInfo.ItemsSource = _activeSale;

            NewSale();
            // RecoverSale();

        }

        private void NewSale()
        {
            _activeSaleTotal = 0;
            _activeSale.Clear();
            dbgActiveSaleInfo.AutoGenerateColumns = true;
            dbgActiveSaleInfo.Columns.Clear();
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

        private void UpdateActiveSaleTotal(double _lineItemPrice)
        {
            _activeSaleTotal += _lineItemPrice;

            lblCurrentSaleTotal.Text = _activeSaleTotal.ToString();
        }

       
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

            UpdateActiveSaleTotal(_price);
        }


        public static byte[] GetHashByteArray(string _unhashedString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(_unhashedString));
        }

        public static string GetHashFromString(string _unhashedString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHashByteArray(_unhashedString))
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
                    //ToDo : Optimize loading of DB. Maybe not use using statement? 
                    using (var _localcontextStock = new SalesContext())
                    {

                        var stockItem = _localcontextStock.Stock
                            .Single(x => x.SKU == edtSKU.Text);

                        AddLinetoActiveSale("001", stockItem.SKU, Convert.ToDouble(edtQtyNumber.Text), stockItem.SellPrice, stockItem.Description);
                    }

                    NewLineItem();

                }
                catch (InvalidOperationException InvalidOpEx)
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
        #endregion

        #region DB Interaction

        private void InitDB()
        {
            //We will load the sales table before we post.
            //_contextSales.Stock.Load();
            _activeSale = new List<databaseAPI.Models.Sale>();
        }



        private void PostSaletoDB(List<databaseAPI.Models.Sale> SaleInfo)
        {
            using (var _localcontextSales = new SalesContext())
            {
                // var blog = new Blog { Url = "http://example.com" };
                foreach (databaseAPI.Models.Sale SingleSaleItem in SaleInfo)
                {
                    _localcontextSales.Sales.Add(SingleSaleItem);
                    _localcontextSales.SaveChanges();
                }
            }
        }


        #endregion
    }
}

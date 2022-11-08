using mainModules.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for stock_mgmt.xaml
    /// </summary>
    public partial class stock_mgmt : Window
    {
        public List<StockItem> _stockList = new List<StockItem>();
        private List<string> _comments = new List<string>();
        private static string _flatFileDelimeter = "#";

        private StockContext _contextStock =
           new StockContext();

        public stock_mgmt()
        {
            InitializeComponent();
            dbgStock.AutoGenerateColumns = true;
            RefreshDataGrid();
            InitDB();
        }

        private void ImportStocktoGrid(string _sku, string _qtyonhand, string _price, string _descr)
        {
            var currStockItem = new StockItem();
            currStockItem.addStockItem(_sku, _qtyonhand, _price, _descr);

            _stockList.Add(currStockItem);
            RefreshDataGrid();


        }

        public void RefreshDataGrid()
        {

            dbgStock.Columns.Clear();

            dbgStock.ItemsSource = null;
            dbgStock.ItemsSource = _stockList;
        }

        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveStockFile(@"c:\temp\stock000.txt");

        }


        private void SaveStockFile(string _pathToStockFile)
        {

            //ToDo: File not found exception
            File.Delete(_pathToStockFile);


            foreach (string currComment in _comments)
            {
                //Save stock list to file
                using (StreamWriter _stockListFile = new StreamWriter(_pathToStockFile, append: true))
                {
                    _stockListFile.WriteLine(currComment);
                }
            }

            using (StreamWriter _stockListFile = new StreamWriter(_pathToStockFile, append: true))
            {
                _stockListFile.WriteLine("code#onhand#retailsprice#description");
            }

            foreach (StockItem _stockItem in _stockList)
            {


                string _currLine = $"{_stockItem.Product_SKU}#{_stockItem.Qty_OnHand}#{_stockItem.RetailPrice}#{_stockItem.Item_Description}";

                //Save stock list to file
                using (StreamWriter _stockListFile = new StreamWriter(_pathToStockFile, append: true))
                {
                    _stockListFile.WriteLine(_currLine);
                }
            }


        }


        private void btnAddStockItem_Click(object sender, RoutedEventArgs e)
        {
            frmstock_entry StockEntryForm = new frmstock_entry();
            StockEntryForm.WindowTitle = "Add New Stock Item";
            StockEntryForm.NewStockItem();
            StockEntryForm.Show();

        }

        private void btnEditStockItem_Click(object sender, RoutedEventArgs e)
        {
            frmstock_entry StockEntryForm = new frmstock_entry();
            
            //ToDo: Out of range exception when no data.
            StockEntryForm.WindowTitle = $"Edit Item {_stockList[dbgStock.SelectedIndex].Product_SKU}";

            StockEntryForm.LoadStockItem(_stockList[dbgStock.SelectedIndex], dbgStock.SelectedIndex);
            StockEntryForm.Show();
        }

        private void btnSaveStockFile_Click(object sender, RoutedEventArgs e)
        {
            SaveStockFile(@"c:\temp\stock000.txt");
        }

        private void btnReloadStockFile_Click(object sender, RoutedEventArgs e)
        {
            //Reload DB using EF Core
        }

        private void btnDeleteSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (dbgStock.SelectedIndex < _stockList.Count)
            {
                _stockList.RemoveAt(dbgStock.SelectedIndex);
                RefreshDataGrid();
            }

        }


        private void InitDB()
        {
            //TODO : Always ensure the DB folder exists
            _contextStock.Database.EnsureCreated();
            _contextStock.Stock.Load();
        }

    }
}

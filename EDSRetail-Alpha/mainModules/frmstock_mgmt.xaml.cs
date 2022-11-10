using mainModules.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;

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

        private CollectionViewSource stockViewSource;

        public stock_mgmt()
        {
            InitializeComponent();

            stockViewSource =
                (CollectionViewSource)FindResource(nameof(stockViewSource));

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
            dbgStock.Items.Refresh();
        }

        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // clean up database connections
            _contextStock.Dispose();
            base.OnClosing(e);
        }


        private void SaveStockFile()
        {

            // all changes are automatically tracked, including
            // deletes!
            _contextStock.SaveChanges();

            // this forces the grid to refresh to latest values
            dbgStock.Items.Refresh();
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
            SaveStockFile();
        }

        private void btnReloadStockFile_Click(object sender, RoutedEventArgs e)
        {
            stockViewSource.Source = null;
            InitDB();
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

            // bind to the source
            stockViewSource.Source =
                _contextStock.Stock.Local.ToObservableCollection();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitDB();
        }

    }
}

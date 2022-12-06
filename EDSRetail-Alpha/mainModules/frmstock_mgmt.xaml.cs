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
        private List<string> _comments = new List<string>();
        private static string _flatFileDelimeter = "#";

        private StockContext _contextStock;

        private CollectionViewSource stockViewSource;

        public stock_mgmt()
        {
            InitializeComponent();

            stockViewSource =
                (CollectionViewSource)FindResource(nameof(stockViewSource));

        }


        public void RefreshDataGrid()
        {
            dbgStock.Items.Refresh();
        }

        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // clean up database connections
            _contextStock.Dispose();
        }



        private void btnAddStockItem_Click(object sender, RoutedEventArgs e)
        {
            frmstock_entry StockEntryForm = new frmstock_entry();
            StockEntryForm.WindowTitle = "Add New Stock Item";
            StockEntryForm.NewStockItem();
            StockEntryForm.Show();

        }


        private void btnSaveStockFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mrConfirmSave = MessageBox.Show($"Are you sure you'd like to post your changes to the database?",
                "Confirm Save Request", MessageBoxButton.YesNo);

            if (mrConfirmSave == MessageBoxResult.Yes)
            {
                // all changes are automatically tracked, including
                // deletes!
                _contextStock.SaveChanges();

                // this forces the grid to refresh to latest values
                dbgStock.Items.Refresh();
            }
        }

        private void btnReloadStockFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mrConfirmReload = MessageBox.Show($"All unsaved changes will be lost.\nContinue?",
                    "Confirm table refresh", MessageBoxButton.YesNo);

            if (mrConfirmReload == MessageBoxResult.Yes)
            {
                _contextStock.Dispose();
                InitDB();
            }

        }

        private void btnDeleteSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (dbgStock.SelectedIndex >= 0)
            {

                databaseAPI.Models.Stock stockItem = dbgStock.SelectedItem as databaseAPI.Models.Stock;

                MessageBoxResult mrConfirmDelete = MessageBox.Show($"Are you sure you'd like to stage this item for deletion?\nSKU = {stockItem.SKU}", 
                    "WARNING: Confirm Item Deletion", MessageBoxButton.YesNo);

                if (mrConfirmDelete == MessageBoxResult.Yes)
                {
                    _contextStock.Stock.Remove(stockItem);
                }

            }

        }


        private void InitDB()
        {
            _contextStock = new StockContext();
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

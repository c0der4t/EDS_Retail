using mainModules.Models;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

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


        public stock_mgmt()
        {
            InitializeComponent();
            dbgStock.AutoGenerateColumns = true;
            RefreshDataGrid();
            LoadStockFile(@"c:\temp\stock000.txt");
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

        private void LoadStockFile(string _pathToStockFile)
        {


            _comments.Clear();
            _stockList.Clear();

            if (File.Exists(_pathToStockFile) == true)
            {
                //Load file
                string currLine = "";
                int lineCount = 0;

                using (StreamReader _stockFlatFile = new StreamReader(_pathToStockFile))
                {
                    while ((currLine = _stockFlatFile.ReadLine()) != null)
                    {
                        lineCount = currLine[0] != '#' ? lineCount += 1 : lineCount = lineCount;

                        if (currLine[0] == '#')
                        {
                            _comments.Add(currLine);
                        }

                        //Checks if we are on line on
                        //If so, skips import
                        //Line one is headings line
                        if ((lineCount > 1) && (currLine[0] != '#'))
                        {
                            string _sku = "";
                            string _qtyonhand = "";
                            string _price = "";
                            string _descr = "";

                            int lengthCurrLine = currLine.Length;
                            int colNumber = 0;


                            while (lengthCurrLine > 0)
                            {
                                colNumber += 1;
                                int indexOfDelimeter = currLine.IndexOf(_flatFileDelimeter) > -1 ? currLine.IndexOf(_flatFileDelimeter) : lengthCurrLine;
                                string currColValue = currLine.Substring(0, indexOfDelimeter);

                                currLine = currLine.IndexOf(_flatFileDelimeter) > -1 ? currLine.Substring(currColValue.Length + _flatFileDelimeter.Length) : "";
                                lengthCurrLine = currLine.Length;

                                switch (colNumber)
                                {
                                    case 1:
                                        _sku = currColValue;
                                        break;
                                    case 2:
                                        _qtyonhand = currColValue;
                                        break;
                                    case 3:
                                        _price = currColValue;
                                        break;
                                    case 4:
                                        _descr = currColValue;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            ImportStocktoGrid(_sku, _qtyonhand, _price, _descr);
                        }


                    }
                }

            }

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
            LoadStockFile(@"c:\temp\stock000.txt");
        }

        private void btnDeleteSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (dbgStock.SelectedIndex < _stockList.Count)
            {
                _stockList.RemoveAt(dbgStock.SelectedIndex);
                RefreshDataGrid();
            }

        }


    }
}

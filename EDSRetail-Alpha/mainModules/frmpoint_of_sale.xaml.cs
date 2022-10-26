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

namespace mainModules
{
    /// <summary>
    /// Interaction logic for point_of_sale.xaml
    /// </summary>
    public partial class point_of_sale : Window
    {
        private List<SaleLineItem> _activeSale;
        private double _activeSaleTotal;
        private static string _flatFileDelimeter = "#";


        public point_of_sale()
        {
            InitializeComponent();

            _activeSale = new List<SaleLineItem>();
            dbgActiveSaleInfo.ItemsSource = _activeSale;
            NewSale();
            RecoverSale();

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
                    _tempSalesFile.WriteLine($"{edtSKU.Text}#{edtQtyNumber.Text}#{edtPrice.Text}#{edtProductDescr.Text}");
                }

                AddLinetoActiveSale(edtSKU.Text, edtQtyNumber.Text, edtPrice.Text, edtProductDescr.Text);

                NewLineItem();
            }



        }

        private void AddLinetoActiveSale(string _sku, string _qty, string _price, string _descr)
        {
            var currLineItem = new SaleLineItem();
            currLineItem.addToSale(_sku, _qty, _price, _descr);

            _activeSale.Add(currLineItem);

            dbgActiveSaleInfo.ItemsSource = null;
            dbgActiveSaleInfo.ItemsSource = _activeSale;

            UpdateActiveSaleTotal(_price);
        }


        private void RecoverSale()
        {
            if (File.Exists(@"C:\temp\sale000.txt") == true)
            {
                var recoverDlgResult = MessageBox.Show("A failed sale has been found, do you want to recover it?",
                     "Recover sale", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (recoverDlgResult == MessageBoxResult.Yes)
                {
                    //Load file
                    string currLine = "";

                    using (StreamReader _recoveredSaleFile = new StreamReader(@"C:\temp\sale000.txt"))
                    {
                        while ((currLine = _recoveredSaleFile.ReadLine()) != null)
                        {
                            string _sku = "";
                            string _qty = "";
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
                                        _qty = currColValue;
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

                            AddLinetoActiveSale(_sku, _qty, _price, _descr);

                        }
                    }


                    // #Twitch
                    //Parse file
                    //Load items into sale
                    //Update live sale total
                }
            }
            else
            {
                NewSale();
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
            File.Delete(@"C:\temp\sale000.txt");
            NewSale();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace point_of_sale_module
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<DataTable> ActiveSale;

        public MainWindow()
        {
            InitializeComponent();
            NewSale();
        }

        private void NewSale()
        {
            ActiveSale.Columns.Add("SKU");
            ActiveSale.Columns.Add("QTY");
            ActiveSale.Columns.Add("Price");
            ActiveSale.Columns.Add("Descr");

            dbgActiveSaleInfo.AutoGenerateColumns = true;
            dbgActiveSaleInfo.Columns.Clear();
            dbgActiveSaleInfo.ItemsSource = ActiveSale;
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
            if (e.Key == Key.Enter)
            {
                var NewSaleItem = ActiveSale.NewRow();

                NewSaleItem[0] = edtSKU.Text ;
                NewSaleItem[0] = edtQtyNumber.Text ;
                NewSaleItem[0] = edtPrice.Text;
                NewSaleItem[0] = redtProductDescr.Document.Blocks.ToString();

                ActiveSale.Rows.Add(NewSaleItem);

                NewSKU();
            }
        }
    }
}

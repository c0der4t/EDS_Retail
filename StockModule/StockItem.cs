using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule
{
    public class StockItem
    {
        private string _sku;
        private string _qtyonhand;
        private string _price;
        private string _descr;


        public string Product_SKU { get { return _sku; } }
        public string Qty_OnHand { get { return _qtyonhand; } }
        public string RetailPrice { get { return _price; } }
        public string Item_Description { get { return _descr; } }

        public void addStockItem(string SKU, string QtyOnHand, string Price, string Descrip)
        {
            _sku = SKU;
            _qtyonhand = QtyOnHand;
            _price = Price;
            _descr = Descrip;
        }



    }
}

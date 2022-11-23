using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainModules.Models
{
    public class SaleLineItem
    {
        private string _saleid;
        private string _salehash;
        private string _sku;
        private string _qty;
        private string _price;
        private string _descr;


        public string SalesID { get { return _saleid; } }
        public string SalesHash { get { return _salehash; } }
        public string Sku { get { return _sku; } }
        public string Qty { get { return _qty; } }
        public string Price { get { return _price; } }
        public string Descript { get { return _descr; } }

        public void addToSale(string Salesid, string SKU, string QTY, string Price, string Descrip)
        {
            _saleid = Salesid;
            //Calculate Hash for SalesHash
            _sku = SKU;
            _qty = QTY;
            _price = Price;
            _descr = Descrip;
        }

    }
}

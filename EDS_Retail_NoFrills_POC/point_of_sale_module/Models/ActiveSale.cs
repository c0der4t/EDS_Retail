using System;
using System.Collections.Generic;
using System.Text;

namespace point_of_sale_module.Models
{
    public class ActiveSale
    {
        private List<SaleLineItem> _allLineItems;

        public List<SaleLineItem> AllLineItems
        {
            get { return _allLineItems; }
            set {

                _allLineItems.Add(value);
            
            
            }
        }









    }


}

using Caliburn.Micro;
using EDS_MVVM_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDS_MVVM_Test.ViewModels
{
    public class ShellViewModel : Screen
    {

        private string _sku;
        private float _qty;
        private float _price;
        private string _itemDescription;
        private SaleLineItemModel _lineItem;
        private BindableCollection<SaleLineItemModel> _lineItems = new BindableCollection<SaleLineItemModel>();


        public string Sku
        {
            get { return _sku; }
            set { _sku = value; }
        }

        public float Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string ItemDescription
        {
            get { return _itemDescription; }
            set { _itemDescription = value; }
        }

        public BindableCollection<SaleLineItemModel> LineItems
        {
            get { return _lineItems; }
            set { _lineItems = value; }
        }

        public SaleLineItemModel LineItem
        {
            get { return _lineItem; }

            set
            {

                _lineItem = value;
                NotifyOfPropertyChange(() => LineItem);
            }
        }


        //Tomorrow on Twitch
        // Bind to Datagrid
        // Update Model in real time as sales come in
        // Write model out to file


    }
}

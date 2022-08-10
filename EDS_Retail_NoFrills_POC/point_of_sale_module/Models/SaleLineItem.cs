namespace point_of_sale_module.Models
{
    public class SaleLineItem
    {
        public int Id { get; set; }
        private string _sku;
        private string _qty;
        private string _price;
        private string _descr;

        public void addToSale(string SKU, string QTY, string Price, string Descrip)
        {
            _sku = SKU;
            _qty = QTY;
            _price = Price;
            _descr = Descrip;
        }

    }
}
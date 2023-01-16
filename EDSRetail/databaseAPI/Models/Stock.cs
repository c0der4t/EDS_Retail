namespace databaseAPI.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public double QTYOnHand { get; set; }
        public double SellPrice { get; set; }
        public double CostPrice { get; set; }

        public bool CanTrade { get; set; }
        public bool ScaleItem { get; set; }

    }
}

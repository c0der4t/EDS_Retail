namespace databaseAPI.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public float QTYOnHand { get; set; }
        public float SellPrice { get; set; }
        public float CostPrice { get; set; }

        public bool CanTrade { get; set; }
        public bool ScaleItem { get; set; }

    }
}

namespace databaseAPI.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public string SaleID { get; set; }
        public string SaleIDHASH { get; set; }
        public string SKU { get; set; }
        public float QTY { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

    }
}

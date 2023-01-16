namespace databaseAPI.Models
{
    public class Sale
    {
        public string SKU { get; set; }
        public double QTY { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double LineTotal { get; set; }
        public string SaleID { get; set; }
        public string SaleIDHASH { get; set; }
        public int ID { get; set; }
        public DateTime SaleDateTime { get; set; }

    }
}

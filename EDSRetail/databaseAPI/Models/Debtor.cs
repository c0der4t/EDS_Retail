namespace databaseAPI.Models
{
    public class Debtor
    {

        public int ID { get; set; }

        public string AccountNumber { get; set; }
        public string AccName { get; set; }
        public string Address { get; set; }
        public string VatNum { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string APEmail { get; set; }
        public string OurAccNum { get; set; }

        public double AccountBalance { get; set; }

    }
}

namespace databaseAPI.Models
{
    public class SystemAudit
    {
        public int ID { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string Username { get; set; }
        public string ElevatedBy { get; set; }
        public string AdditionalInfo { get; set; }

    }
}

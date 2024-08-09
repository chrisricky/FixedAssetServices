namespace FixedAssetServices.Models
{
    public class InsFAMaintenanceRequest
    {
        public string? Id { get; set; }
        public decimal? MaintAmount { get; set; }
        public string? MaintDesc { get; set; }
        public DateTime? MaintDate { get; set; }
        //public int? VendorID { get; set; }
        public string? FundSourceGL { get; set; }
        public string? UserID { get; set; }
        public string? AuthID { get; set; }
    }
}

namespace FixedAssetServices.Models
{
    public class InsFARepairsRequest
    {
        public string Id { get; set; }
        public decimal RepairAmount { get; set; }
        public string RepairDesc { get; set; }
        public DateTime RepairDate { get; set; }
        public int Capitalized { get; set; }
        public int AddedLifeSpan { get; set; }
        //public int VendorID { get; set; }
        public string FundSourceGL { get; set; }
        public string UserID { get; set; }
        public string AuthID { get; set; }
    }
}

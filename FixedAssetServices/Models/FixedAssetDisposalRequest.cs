namespace FixedAssetServices.Models
{
    public class FixedAssetDisposalRequest
    {
        public string Id { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal NetBookVal { get; set; }
        public string SalesGL { get; set; }
        public string SalesIncExpGL { get; set; }
        public string UserId { get; set; }
        public string AuthId { get; set; }
    }
}

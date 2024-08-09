namespace FixedAssetServices.Models
{
    public class FixedAssetDisposalListRequest
    {
        public List<FixedAssetDisposalItem> FixedAssetDisposalItems { get; set; }
        public DateTime? TranDate { get; set; }
        public string? UserId { get; set; }
        public string? AuthId { get; set; }
    }
}

namespace FixedAssetServices.Models
{
    public class FetchExpFixedAssetsByCatResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public List<FixedAssetCat>? FixedAssets { get; set; }
    }
}

namespace FixedAssetServices.Models
{
    public class FixedAssestResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public FixedAssetDetails? FixedAsset { get; set; }
    }
}

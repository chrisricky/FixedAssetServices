namespace FixedAssetServices.Models
{
    public class BranchResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public List<Branch>? Branches { get; set; }
    }
}

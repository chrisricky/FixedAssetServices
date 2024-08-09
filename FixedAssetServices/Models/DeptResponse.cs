namespace FixedAssetServices.Models
{
    public class DeptResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public List<Department>? departments { get; set; }
    }
}

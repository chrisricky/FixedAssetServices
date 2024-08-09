namespace FixedAssetServices.Models
{
    public class UserResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public List<Users>? Users { get; set; }
    }
}

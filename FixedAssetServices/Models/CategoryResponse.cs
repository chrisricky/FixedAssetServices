namespace FixedAssetServices.Models
{
    public class CategoryResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public List<Category> Categories { get; set; }
    }
}

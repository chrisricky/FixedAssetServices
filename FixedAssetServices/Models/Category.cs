using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    [Keyless]
    public class Category
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}

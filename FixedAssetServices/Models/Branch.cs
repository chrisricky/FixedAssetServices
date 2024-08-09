using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    [Keyless]
    public class Branch
    {
        public string? BranchCode { get; set; }
        public string? BranchName { get; set; }
    }
}

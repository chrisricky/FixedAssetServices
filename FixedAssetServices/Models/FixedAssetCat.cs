using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    [Keyless]
    public class FixedAssetCat
    {
        public string? Id { get; set; }
        public string? AssetName { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? DepreciationStartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public decimal? AccumDepreciation { get; set; }
        public int? LifespanMonths { get; set; }
        public int? DepreciatedLife { get; set; }
        public decimal? NetBookVal { get; set; }
        public int? SalesAmount { get; set; }
        public int? ProfitLoss { get; set; }
    }
}

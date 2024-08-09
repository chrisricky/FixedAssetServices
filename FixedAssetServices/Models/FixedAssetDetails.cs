using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    [Keyless]
    public class FixedAssetDetails
    {
        public string? Categorycode { get; set; }
        public string? AssetNumber { get; set; }
        public string? AssetName { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? AssetGL { get; set; }
        public string? SourceOfFunds { get; set; }
        public decimal? MonthlyDepreciation { get; set; }
        public decimal? NetBookValue { get; set; }
        public decimal? AssetCost { get; set; }
        public decimal? AccumDepreciation { get; set; }
        public string? AssetStatus { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastDepDate { get; set; }
        public int? LifespanMonths { get; set; }
        public int? UsedLifeSpan { get; set; }
        public string? Branchcode { get; set; }
        public decimal? DepreciationRate { get; set; }
        public DateTime? DepreciationStartDate { get; set; }
        public DateTime? Expirydate { get; set; }
    }
}

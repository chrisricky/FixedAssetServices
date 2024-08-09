using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    public class FixedAssetDisposalItem
    {
        public string Id { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal NetBookVal { get; set; }
        public string SalesGL { get; set; }
        public string SalesIncExpGL { get; set; }
    }
}

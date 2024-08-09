using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    public partial class FixedAssestContext : DbContext
    {
        public FixedAssestContext(DbContextOptions<FixedAssestContext> options)
           : base(options)
        {
        }


        public DbSet<FixedAssetDetails> FixedAssetDetails { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FixedAssetCat> FixedAssetCat { get; set; }
    }
}

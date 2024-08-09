using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    [Keyless]
    public class Users
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
    }
}

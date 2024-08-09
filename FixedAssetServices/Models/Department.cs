using Microsoft.EntityFrameworkCore;

namespace FixedAssetServices.Models
{
    [Keyless]
    public class Department
    {
        public string? deptid { get; set; }
        public string? DeptName { get; set; }
    }
}

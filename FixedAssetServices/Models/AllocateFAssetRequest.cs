namespace FixedAssetServices.Models
{
    public class AllocateFAssetRequest
    {
        public string Id { get; set; }
        public string Branchcode { get; set; }
        public string DeptID { get; set; }
        public string OfficerID { get; set; }
        public DateTime DateAllocated { get; set; }
        public string UserID { get; set; }
        public string AuthID { get; set; }
        public int ActionType { get; set; }
    }
}

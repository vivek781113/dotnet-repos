namespace DataAccessLayer.Models
{
    public class CartDetail
    {
        public long CartId { get; }
        public string CartName { get; }
        public string CartCreatedBy { get; }
        public string CartCreatedOn { get; }
        public long CartValue { get;}
        public string CartStatusId { get; }
        public string CartStatusDescription { get;}
        public string DepartmentId { get; }
        public string DepartmentDescription { get; }

    }
}

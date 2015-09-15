namespace CallTracking.Web.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int LeadSourceId { get; set; }
        public virtual LeadSource LeadSource { get; set; }
    }
}

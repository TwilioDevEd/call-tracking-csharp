using System.Data.Entity;

namespace CallTracking.Web.Models
{
    public class CallTrackingContext : DbContext
    {
        public CallTrackingContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<LeadSource> LeadSources { get; set; }
        public DbSet<Lead> Leads { get; set; }
    }
}
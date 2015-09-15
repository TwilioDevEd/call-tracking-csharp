using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallTracking.Web.Models
{
    public class LeadSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IncomingNumberNational { get; set; }
        public string IncomingNumberInternational { get; set; }
        [Display(Name = "Forwarding Number")]
        public string ForwardingNumber { get; set; }
        public virtual IList<Lead> Leads { get; set; }
    }
}

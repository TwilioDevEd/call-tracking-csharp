using System.Linq;
using System.Web.Mvc;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;

namespace CallTracking.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IRepository<Lead> _leadsRepository;

        public StatisticsController()
            : this(new LeadsRepository()) { }

        public StatisticsController(IRepository<Lead> leadsRepository)
        {
            _leadsRepository = leadsRepository;
        }

        // GET: Statistics/LeadsBySource
        public ActionResult LeadsBySource()
        {
            var leads = _leadsRepository.All().Select(x => new {x.LeadSource.Name});
            var response = leads.GroupBy(x => x.Name).Select(group => new {Label = group.Key, Value = group.Count()}).ToList();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/LeadsByCity
        public ActionResult LeadsByCity()
        {
            var response = _leadsRepository.All().GroupBy(x => x.City).Select(group => new { Label = group.Key, Value = group.Count() });
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
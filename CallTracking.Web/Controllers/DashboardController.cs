using System.Web.Mvc;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;

namespace CallTracking.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRepository<LeadSource> _repository;


        public DashboardController()
            : this(new LeadSourcesRepository()) { }

        public DashboardController(IRepository<LeadSource> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            var leadSources = _repository.All();
            return View(leadSources);
        }
    }
}
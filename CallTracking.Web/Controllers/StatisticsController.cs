using System;
using System.Collections.Generic;
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
            var response = leads.GroupBy(x => x.Name).Select(group => new {label = group.Key, value = group.Count()}).ToDictionary(x => x.label, x => x.value);
    
            return Json(GetChartData(response), JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/LeadsByCity
        public ActionResult LeadsByCity()
        {
            var response = _leadsRepository.All().GroupBy(x => x.City).Select(group => new { label = group.Key, value = group.Count() }).ToDictionary(x => x.label, x => x.value);

            return Json(GetChartData(response), JsonRequestBehavior.AllowGet);
        }

        private Object GetChartData(Dictionary<string, int> response)
        {
            return new
            {
                datasets = new[] { new { data = response.Values.ToArray(), backgroundColor = GetColors(response.Count) } },
                labels = response.Keys.ToArray()
            };
        }

        private string[] GetColors(int quantity)
        {
            string[] colors = {
              "#eddcd2ff",
              "#fff1e6ff",
              "#fde2e4ff",
              "#fad2e1ff",
              "#c5deddff",
              "#dbe7e4ff",
              "#f0efebff",
              "#d6e2e9ff",
              "#bcd4e6ff",
              "#99c1deff"
            };
            Array.Reverse(colors);
            return new ArraySegment<string>(colors, 0, quantity).ToArray();
        }
    }
}
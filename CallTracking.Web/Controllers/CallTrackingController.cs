using System.Web.Mvc;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Twilio.TwiML;

namespace CallTracking.Web.Controllers
{
    public class CallTrackingController : Controller
    {
        private readonly IRepository<LeadSource> _leadSourcesRepository;
        private readonly IRepository<Lead> _leadsRepository;

        public CallTrackingController()
            : this(new LeadSourcesRepository(), new LeadsRepository()) { }

        public CallTrackingController(IRepository<LeadSource> leadSourcesRepository, IRepository<Lead> leadsRepository)
        {
            _leadSourcesRepository = leadSourcesRepository;
            _leadsRepository = leadsRepository;
        }

        // POST: CallTracking/ForwardCall
        [HttpPost]
        public ActionResult ForwardCall(string called, string caller, string fromCity, string fromState)
        {
            var leadSource = _leadSourcesRepository.FirstOrDefault(x => x.IncomingNumberInternational == called);
            _leadsRepository.Create(new Lead
            {
                LeadSourceId = leadSource.Id,
                City = fromCity,
                State = fromState,
                PhoneNumber = caller
            });

            var response = new VoiceResponse();
            var dial = new Dial();
            dial.Number(leadSource.ForwardingNumber);

            response.Dial(dial);

            return Content(response.ToString(), "text/xml");
        }
    }
}
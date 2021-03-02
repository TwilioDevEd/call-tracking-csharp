using System.Web.Mvc;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace CallTracking.Web.Controllers
{
    public class CallTrackingController : TwilioController
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

            response.Append(dial);

            return TwiML(response);
        }
    }
}
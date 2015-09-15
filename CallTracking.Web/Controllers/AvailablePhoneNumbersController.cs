using System.Web.Mvc;
using CallTracking.Web.Domain.Twilio;
using Twilio;

namespace CallTracking.Web.Controllers
{
    public class AvailablePhoneNumbersController : Controller
    {
        // GET: AvailablePhoneNumbers
        public ActionResult Index(string areaCode)
        {
            var client = new TwilioRestClient(Credentials.TwilioAccountSid, Credentials.TwilioAuthToken);
            var phoneNumbers = new RestClient(client).SearchPhoneNumbers();

            return View(phoneNumbers);
        }
    }
}
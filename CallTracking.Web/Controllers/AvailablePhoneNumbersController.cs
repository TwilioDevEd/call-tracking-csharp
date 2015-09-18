using System.Web.Mvc;
using CallTracking.Web.Domain.Twilio;

namespace CallTracking.Web.Controllers
{
    public class AvailablePhoneNumbersController : Controller
    {
        private readonly IRestClient _restClient;

        public AvailablePhoneNumbersController()
            : this(new RestClient())
        {}

        public AvailablePhoneNumbersController(IRestClient restClient)
        {
            _restClient = restClient;
        }

        // GET: AvailablePhoneNumbers
        public ActionResult Index(string areaCode)
        {
            var phoneNumbers = _restClient.SearchPhoneNumbers(areaCode);

            return View(phoneNumbers);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Twilio;

namespace CallTracking.Web.Domain.Twilio
{
    public interface IRestClient
    {
        IEnumerable<AvailablePhoneNumber> SearchPhoneNumbers(string areaCode);
        IncomingPhoneNumber PurchasePhoneNumber(string phoneNumber, string applicationSid);
        string GetApplicationSid();
    }

    public class RestClient : IRestClient
    {
        private readonly TwilioRestClient _client;

        public RestClient(TwilioRestClient client)
        {
            _client = client;
        }

        public IEnumerable<AvailablePhoneNumber> SearchPhoneNumbers(string areaCode = "415")
        {
            var searchParams = new AvailablePhoneNumberListRequest
            {
                AreaCode = areaCode
            };

            return _client.ListAvailableLocalPhoneNumbers("US", searchParams)
                .AvailablePhoneNumbers;
        }

        public IncomingPhoneNumber PurchasePhoneNumber(string phoneNumber, string applicationSid)
        {
            var phoneNumberOptions = new PhoneNumberOptions
            {
                PhoneNumber = phoneNumber,
                VoiceApplicationSid = applicationSid
            };

            return _client.AddIncomingPhoneNumber(phoneNumberOptions);
        }

        public string GetApplicationSid()
        {
            const string defaultApplicationName = "Call tracking app";
            var application = _client.ListApplications(defaultApplicationName, null, null)
                .Applications.FirstOrDefault();

            return application != null
                ? application.Sid
                : _client.AddApplication(defaultApplicationName, new ApplicationOptions()).Sid;
        }
    }
}
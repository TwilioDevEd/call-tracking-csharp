using System.Linq;
using System.Threading.Tasks;
using Twilio.Base;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Api.V2010.Account.AvailablePhoneNumberCountry;
using Twilio.Types;

namespace CallTracking.Web.Domain.Twilio
{
    public interface IRestClient
    {
        Task<ResourceSet<LocalResource>> SearchPhoneNumbersAsync(string areaCode);
        Task<IncomingPhoneNumberResource> PurchasePhoneNumberAsync(string phoneNumber, string applicationSid);
        Task<string> GetApplicationSidAsync();
    }

    public class RestClient : IRestClient
    {
        private readonly ITwilioRestClient _client;
        
        public RestClient()
        {
            _client = new TwilioRestClient(Credentials.TwilioAccountSid, Credentials.TwilioAuthToken);
        }

        public RestClient(ITwilioRestClient client)
        {
            _client = client;
        }

        public async Task<ResourceSet<LocalResource>> SearchPhoneNumbersAsync(string areaCode = "415")
        {
            return await LocalResource.ReadAsync(countryCode: "US", areaCode: int.Parse(areaCode), client: _client);
        }

        public async Task<IncomingPhoneNumberResource> PurchasePhoneNumberAsync(string phoneNumber, string applicationSid)
        {
            return await IncomingPhoneNumberResource.CreateAsync(
                voiceApplicationSid: applicationSid,
                phoneNumber: new PhoneNumber(phoneNumber),
                client: _client
            );
        }

        public async Task<string> GetApplicationSidAsync()
        {
            const string defaultApplicationName = "Call tracking app";
            var applications = await ApplicationResource.ReadAsync(defaultApplicationName, client: _client);

            var application = applications.FirstOrDefault() ?? 
                await ApplicationResource.CreateAsync(defaultApplicationName);

            return application.Sid;
        }
    }
}
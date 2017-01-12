using System.Collections.Generic;
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
        Task<IEnumerable<LocalResource>> SearchPhoneNumbersAsync(string areaCode);
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

        public async Task<IEnumerable<LocalResource>> SearchPhoneNumbersAsync(string areaCode = "415")
        {
            var localNumbers = await LocalResource.ReadAsync(
                countryCode: "US", areaCode: int.Parse(areaCode), client: _client);

            return localNumbers.ToList();
        }

        public async Task<IncomingPhoneNumberResource> PurchasePhoneNumberAsync(
            string phoneNumber, string applicationSid)
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
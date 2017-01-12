using System.Net;
using Moq;
using NUnit.Framework;
using Twilio.Clients;
using Twilio.Http;

namespace CallTracking.Web.Test.Domain.Twilio
{
    public class RestClientTest
    {
        private Mock<ITwilioRestClient> _mockClient;

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<ITwilioRestClient>();
        }

        [Test]
        public async void SearchPhoneNumbers()
        {
            Request savedRequest = null;
            _mockClient.Setup(trc => trc.RequestAsync(It.IsAny<Request>()))
                .Callback<Request>(request => savedRequest = request)
                .ReturnsAsync(new Response(HttpStatusCode.OK,
                    "{\"available_phone_numbers\": [{\"address_requirements\": \"none\",\"beta\": false,\"capabilities\": {\"mms\": true,\"sms\": false,\"voice\": true},\"friendly_name\": \"(808) 925-1571\",\"iso_country\": \"US\",\"lata\": \"834\",\"latitude\": \"19.720000\",\"longitude\": \"-155.090000\",\"phone_number\": \"+18089251571\",\"postal_code\": \"96720\",\"rate_center\": \"HILO\",\"region\": \"HI\"}],\"end\": 1,\"first_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/AvailablePhoneNumbers/US/Local.json?PageSize=50&Page=0\",\"last_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/AvailablePhoneNumbers/US/Local.json?PageSize=50&Page=0\",\"next_page_uri\": null,\"num_pages\": 1,\"page\": 0,\"page_size\": 50,\"previous_page_uri\": null,\"start\": 0,\"total\": 1,\"uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/AvailablePhoneNumbers/US/Local.json?PageSize=1\"}"));

            await new Web.Domain.Twilio.RestClient(_mockClient.Object).SearchPhoneNumbersAsync();

            _mockClient.Verify(trc => trc.RequestAsync(It.IsAny<Request>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }

        [Test]
        public async void PurchasePhoneNumber()
        {
            Request savedRequest = null;
            _mockClient.Setup(trc => trc.RequestAsync(It.IsAny<Request>()))
                .Callback<Request>(request => savedRequest = request)
                .ReturnsAsync(new Response(HttpStatusCode.OK,
                    "{\"date_created\": \"Mon, 22 Aug 2011 20:58:45 +0000\", \"uri\": \"2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/AvailablePhoneNumbers/US/Local.json\"}"));

            await new Web.Domain.Twilio.RestClient(_mockClient.Object).PurchasePhoneNumberAsync("+14152339867", "ApplicationSid");

            _mockClient.Verify(trc => trc.RequestAsync(It.IsAny<Request>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }

        [Test]
        public async void GetApplicationSid_verifies_an_application_result_is_returned_when_listing_aplications()
        {
            Request savedRequest = null;
            _mockClient.Setup(trc => trc.RequestAsync(It.IsAny<Request>()))
                .Callback<Request>((request) => savedRequest = request)
                .ReturnsAsync(new Response(HttpStatusCode.OK, "{\"applications\": [{\"account_sid\": \"ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"api_version\": \"2010-04-01\",\"date_created\": \"Fri, 21 Aug 2015 00:07:25 +0000\",\"date_updated\": \"Fri, 21 Aug 2015 00:07:25 +0000\",\"friendly_name\": \"d8821fb7-4d01-48b2-bdc5-34e46252b90b\",\"message_status_callback\": null,\"sid\": \"APaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"sms_fallback_method\": \"POST\",\"sms_fallback_url\": null,\"sms_method\": \"POST\",\"sms_status_callback\": null,\"sms_url\": null,\"status_callback\": null,\"status_callback_method\": \"POST\",\"uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications/APaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.json\",\"voice_caller_id_lookup\": false,\"voice_fallback_method\": \"POST\",\"voice_fallback_url\": null,\"voice_method\": \"POST\",\"voice_url\": null}],\"end\": 0,\"first_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications.json?PageSize=1&Page=0\",\"last_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications.json?PageSize=1&Page=35\",\"next_page_uri\": null,\"num_pages\": 36,\"page\": 0,\"page_size\": 1,\"previous_page_uri\": null,\"start\": 0,\"total\": 36,\"uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications.json?PageSize=1&Page=0\"}"));

            await new Web.Domain.Twilio.RestClient(_mockClient.Object).GetApplicationSidAsync();

            _mockClient.Verify(trc => trc.RequestAsync(It.IsAny<Request>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }

        [Test]
        public async void GetApplicationSid_verifies_an_application_is_created_if_there_are_no_applications()
        {
            Request savedRequest = null;
            _mockClient.Setup(trc => trc.RequestAsync(It.IsAny<Request>()))
                .Callback<Request>((request) => savedRequest = request)
                .ReturnsAsync(new Response(HttpStatusCode.OK, "{\"applications\": [{\"account_sid\": \"ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"api_version\": \"2010-04-01\",\"date_created\": \"Fri, 21 Aug 2015 00:07:25 +0000\",\"date_updated\": \"Fri, 21 Aug 2015 00:07:25 +0000\",\"friendly_name\": \"d8821fb7-4d01-48b2-bdc5-34e46252b90b\",\"message_status_callback\": null,\"sid\": \"APaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"sms_fallback_method\": \"POST\",\"sms_fallback_url\": null,\"sms_method\": \"POST\",\"sms_status_callback\": null,\"sms_url\": null,\"status_callback\": null,\"status_callback_method\": \"POST\",\"uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications/APaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.json\",\"voice_caller_id_lookup\": false,\"voice_fallback_method\": \"POST\",\"voice_fallback_url\": null,\"voice_method\": \"POST\",\"voice_url\": null}],\"end\": 0,\"first_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications.json?PageSize=1&Page=0\",\"last_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications.json?PageSize=1&Page=35\",\"next_page_uri\": null,\"num_pages\": 36,\"page\": 0,\"page_size\": 1,\"previous_page_uri\": null,\"start\": 0,\"total\": 36,\"uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/Applications.json?PageSize=1&Page=0\"}"));

            await new Web.Domain.Twilio.RestClient(_mockClient.Object).GetApplicationSidAsync();

            _mockClient.Verify(trc => trc.RequestAsync(It.IsAny<Request>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }
    }
}

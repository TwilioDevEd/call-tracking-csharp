using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using CallTracking.Web.Controllers;
using CallTracking.Web.Domain.Twilio;
using Moq;
using NUnit.Framework;
using Twilio.Clients;
using Twilio.Http;
using System.Net;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account.AvailablePhoneNumberCountry;

namespace CallTracking.Web.Test.Controllers
{
    public class AvailablePhoneNumbersControllerTest
    {
        private Mock<ITwilioRestClient> _mockTwilioRestClient;

        [SetUp]
        public void Setup()
        {
            _mockTwilioRestClient = new Mock<ITwilioRestClient>();
        }

        [Test]
        public async void Index_returns_a_list_of_phone_numbers()
        {
            _mockTwilioRestClient.Setup(trc => trc.RequestAsync(It.IsAny<Request>()))
                .ReturnsAsync(new Response(HttpStatusCode.OK,
                    "{\"available_phone_numbers\": [{\"address_requirements\": \"none\",\"beta\": false,\"capabilities\": {\"mms\": true,\"sms\": false,\"voice\": true},\"friendly_name\": \"(808) 925-1571\",\"iso_country\": \"US\",\"lata\": \"834\",\"latitude\": \"19.720000\",\"longitude\": \"-155.090000\",\"phone_number\": \"+18089251571\",\"postal_code\": \"96720\",\"rate_center\": \"HILO\",\"region\": \"HI\"}],\"end\": 1,\"first_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/AvailablePhoneNumbers/US/Local.json?PageSize=50&Page=0\",\"last_page_uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/AvailablePhoneNumbers/US/Local.json?PageSize=50&Page=0\",\"next_page_uri\": null,\"num_pages\": 1,\"page\": 0,\"page_size\": 50,\"previous_page_uri\": null,\"start\": 0,\"total\": 1,\"uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/AvailablePhoneNumbers/US/Local.json?PageSize=1\"}"));

            var _mockRestClient = new RestClient(_mockTwilioRestClient.Object);
            var controller = GetAvailablePhoneNumbersController(_mockRestClient);

            var result = await controller.Index("415") as ViewResult;

            var viewModel = (ResourceSet<LocalResource>)result.ViewData.Model;
            Assert.That(viewModel.First().FriendlyName.ToString(), Is.EqualTo("(808) 925-1571"));
        }

        private static AvailablePhoneNumbersController GetAvailablePhoneNumbersController(RestClient client)
        {
            var controller = new AvailablePhoneNumbersController(client);

            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return controller;
        }
    }
}

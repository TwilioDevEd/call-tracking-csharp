using System.Web.Mvc;
using System.Web.Routing;
using CallTracking.Web.Controllers;
using CallTracking.Web.Domain.Twilio;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using NUnit.Framework;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CallTracking.Web.Test.Controllers
{
    public class LeadSourcesControllerTest
    {
        private Mock<IRestClient> _mockRestClient;
        private Mock<IRepository<LeadSource>> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRestClient = new Mock<IRestClient>();
            var phoneNumber =
                IncomingPhoneNumberResource.FromJson(
                    "{\"account_sid\": \"ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"address_requirements\": \"none\",\"api_version\": \"2010-04-01\",\"beta\": false,\"capabilities\": {\"mms\": true,\"sms\": false,\"voice\": true},\"date_created\": \"Thu, 30 Jul 2015 23:19:04 +0000\",\"date_updated\": \"Thu, 30 Jul 2015 23:19:04 +0000\",\"emergency_status\": \"Active\",\"emergency_address_sid\": \"ADaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"friendly_name\": \"(808) 925-5327\",\"phone_number\": \"+18089255327\",\"sid\": \"PNaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"sms_application_sid\": \"\",\"sms_fallback_method\": \"POST\",\"sms_fallback_url\": \"\",\"sms_method\": \"POST\",\"sms_url\": \"\",\"status_callback\": \"\",\"status_callback_method\": \"POST\",\"trunk_sid\": null,\"uri\": \"/2010-04-01/Accounts/ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/IncomingPhoneNumbers/PNaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.json\",\"voice_application_sid\": \"\",\"voice_caller_id_lookup\": false,\"voice_fallback_method\": \"POST\",\"voice_fallback_url\": null,\"voice_method\": \"POST\",\"voice_url\": null}");

            _mockRestClient.Setup(x => x.PurchasePhoneNumberAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(phoneNumber);

            _mockRepository = new Mock<IRepository<LeadSource>>();
            _mockRepository.Setup(x => x.Create(It.IsAny<LeadSource>()));
            _mockRepository.Setup(x => x.Update(It.IsAny<LeadSource>()));
        }


        [Test]
        public async void Create_creates_a_lead_source()
        {
            var controller = GetLeadSourcesController(_mockRepository.Object, _mockRestClient.Object);

            await controller.Create("+1 555 555 55555");
            _mockRepository.Verify(r => r.Create(It.IsAny<LeadSource>()), Times.Once);
        }

        [Test]
        public async void Create_redirects_to_edit_view_on_success()
        {
            var controller = GetLeadSourcesController(_mockRepository.Object, _mockRestClient.Object);

            var result = await controller.Create("+1 555 555 55555");

            var routeResult = (RedirectToRouteResult)result;
            Assert.That(routeResult.RouteValues["action"], Is.EqualTo("Edit"));
        }

        [Test]
        public void Edit_edits_a_lead_source()
        {
            var controller = GetLeadSourcesController(_mockRepository.Object, _mockRestClient.Object);

            var leadSource = new LeadSource();
            controller.Edit(leadSource);
            _mockRepository.Verify(r => r.Update(It.IsAny<LeadSource>()), Times.Once);
        }

        [Test]
        public void Create_redirects_to_dashboard_on_success()
        {
            var controller = GetLeadSourcesController(_mockRepository.Object, _mockRestClient.Object);
            var result = (RedirectToRouteResult)controller.Edit(new LeadSource());

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Dashboard"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        private static LeadSourcesController GetLeadSourcesController(IRepository<LeadSource> repository, IRestClient client)
        {
            var controller = new LeadSourcesController(repository, client);

            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return controller;
        }
    }
}

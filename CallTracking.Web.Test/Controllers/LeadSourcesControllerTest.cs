using System.Web.Mvc;
using System.Web.Routing;
using CallTracking.Web.Controllers;
using CallTracking.Web.Domain.Twilio;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using NUnit.Framework;
using Twilio;

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
            var phoneNumber = new IncomingPhoneNumber
            {
                PhoneNumber = "+14159699064",
                FriendlyName = "(415) 969-9064"
            };
            _mockRestClient.Setup(x => x.PurchasePhoneNumber(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(phoneNumber);

            _mockRepository = new Mock<IRepository<LeadSource>>();
            _mockRepository.Setup(x => x.Create(It.IsAny<LeadSource>()));
            _mockRepository.Setup(x => x.Update(It.IsAny<LeadSource>()));
        }


        [Test]
        public void Create_creates_a_lead_source()
        {
            var controller = GetLeadSourcesController(_mockRepository.Object, _mockRestClient.Object);

            controller.Create("+1 555 555 55555");
            _mockRepository.Verify(r => r.Create(It.IsAny<LeadSource>()), Times.Once);
        }

        [Test]
        public void Create_redirects_to_edit_view_on_success()
        {
            var controller = GetLeadSourcesController(_mockRepository.Object, _mockRestClient.Object);
            var result = (RedirectToRouteResult) controller.Create("+1 555 555 55555");

            Assert.That(result.RouteValues["action"], Is.EqualTo("Edit"));
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

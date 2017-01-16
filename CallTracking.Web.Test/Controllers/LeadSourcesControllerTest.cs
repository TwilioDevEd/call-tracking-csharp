using CallTracking.Web.Controllers;
using CallTracking.Web.Domain.Twilio;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
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
                IncomingPhoneNumberResource.FromJson("{}");

            _mockRestClient.Setup(x => x.PurchasePhoneNumberAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(phoneNumber);

            _mockRepository = new Mock<IRepository<LeadSource>>();
        }

        [Test]
        public void Create_redirects_to_edit_view_on_success()
        {
            var controller = new LeadSourcesController(_mockRepository.Object, _mockRestClient.Object);

            controller.WithCallTo(c => c.Create("+1 555 555 55555"))
                .ShouldRedirectTo(c => c.Edit(1));

            _mockRepository.Verify(r => r.Create(It.IsAny<LeadSource>()), Times.Once);
        }

        [Test]
        public void Create_redirects_to_dashboard_on_success()
        {
            var controller = new LeadSourcesController(_mockRepository.Object, _mockRestClient.Object);

            controller.WithCallTo(c => c.Edit(new LeadSource()))
                .ShouldRedirectTo<DashboardController>(c => c.Index());

            _mockRepository.Verify(r => r.Update(It.IsAny<LeadSource>()), Times.Once);
        }
    }
}

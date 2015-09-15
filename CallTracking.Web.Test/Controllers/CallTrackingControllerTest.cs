using System;
using System.Web.Mvc;
using System.Web.Routing;
using CallTracking.Web.Controllers;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using NUnit.Framework;

namespace CallTracking.Web.Test.Controllers
{
    public class CallTrackingControllerTest
    {
        private Mock<IRepository<LeadSource>> _mockLeadSourcesRepository;
        private Mock<IRepository<Lead>> _mockLeadsRepository;
        [SetUp]
        public void Setup()
        {
            _mockLeadSourcesRepository = new Mock<IRepository<LeadSource>>();
            _mockLeadSourcesRepository.Setup(x => x.FirstOrDefault(It.IsAny<Func<LeadSource, bool>>()))
                .Returns(new LeadSource { ForwardingNumber = "+12568417333" });

            _mockLeadsRepository = new Mock<IRepository<Lead>>();
            _mockLeadsRepository.Setup(x => x.Create(It.IsAny<Lead>()));
        }

        [Test]
        public void ForwardCall_finds_the_lead_source_for_the_given_called()
        {
            var controller = GetCallTrackingController(_mockLeadSourcesRepository.Object, _mockLeadsRepository.Object);
            controller.ForwardCall("called", "caller", "Modesto", "CA");

            _mockLeadsRepository.Verify(r => r.Create(It.IsAny<Lead>()), Times.Once);
        }

        [Test]
        public void ForwardCall_creates_a_lead()
        {
            var controller = GetCallTrackingController(_mockLeadSourcesRepository.Object, _mockLeadsRepository.Object);
            controller.ForwardCall("called", "caller", "Modesto", "CA");

            _mockLeadsRepository.Verify(r => r.Create(It.IsAny<Lead>()), Times.Once);
        }

        private static CallTrackingController GetCallTrackingController(IRepository<LeadSource> leadSourcesRepository, IRepository<Lead> leadsRepository)
        {
            var controller = new CallTrackingController(leadSourcesRepository, leadsRepository);

            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return controller;
        }
    }
}

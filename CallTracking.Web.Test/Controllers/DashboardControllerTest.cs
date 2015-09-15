using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using CallTracking.Web.Controllers;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using NUnit.Framework;
using Twilio;

namespace CallTracking.Web.Test.Controllers
{
    class DashboardControllerTest
    {
        [Test]
        public void Index_returns_a_list_of_lead_sources()
        {
            var leadSources = new List<LeadSource>();
            var mockRepository = new Mock<IRepository<LeadSource>>();
            mockRepository.Setup(x => x.All()).Returns(leadSources);
            var controller = GetDashboardController(mockRepository.Object);

            var result = controller.Index() as ViewResult;

            Assert.That(result.ViewData.Model, Is.EqualTo(leadSources));
        }

        private static DashboardController GetDashboardController(IRepository<LeadSource> repository)
        {
            var controller = new DashboardController(repository);

            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return controller;
        }
    }
}

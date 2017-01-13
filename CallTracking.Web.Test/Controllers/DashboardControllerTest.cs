using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CallTracking.Web.Controllers;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace CallTracking.Web.Test.Controllers
{
    class DashboardControllerTest
    {
        [Test]
        public void Index_returns_a_list_of_lead_sources()
        {
            var leadSources = new List<LeadSource>()
            {
                new LeadSource
                {
                    Name = "Lead source name",
                    IncomingNumberNational = "+17076472273"
                }
            };
            var mockRepository = new Mock<IRepository<LeadSource>>();
            mockRepository.Setup(x => x.All()).Returns(leadSources);
            var controller = new DashboardController(mockRepository.Object);

            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel(leadSources);
        }
    }
}

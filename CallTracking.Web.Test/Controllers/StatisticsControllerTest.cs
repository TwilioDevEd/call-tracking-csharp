using System.Collections.Generic;
using CallTracking.Web.Controllers;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace CallTracking.Web.Test.Controllers
{
    public class StatisticsControllerTest
    {
        [Test]
        public void LeadsBySource_returns_leads_grouped_by_source()
        {
            var leadSource = new LeadSource {Id = 1, Name = "Downtown"};
            var leads = new List<Lead>
            {
                new Lead {LeadSource = leadSource},
                new Lead {LeadSource = leadSource}
            };

            var mockRepository = new Mock<IRepository<Lead>>();
            mockRepository.Setup(x => x.All()).Returns(leads);

            var controller = new StatisticsController(mockRepository.Object);

            controller.WithCallTo(ctrl => ctrl.LeadsBySource())
                .ShouldReturnJson(data => Assert.That(JArray.FromObject(data)[0]["label"].ToString(), Is.EqualTo("Downtown")));
        }

        [Test]
        public void LeadsBySource_returns_leads_grouped_by_city()
        {
            var leads = new List<Lead>
            {
                new Lead {City = "San Diego"},
                new Lead {City = "San Diego"},
                new Lead {City = "Modesto"}
            };

            var mockRepository = new Mock<IRepository<Lead>>();
            mockRepository.Setup(x => x.All()).Returns(leads);

            var controller = new StatisticsController(mockRepository.Object);

            controller.WithCallTo(ctrl => ctrl.LeadsByCity())
                .ShouldReturnJson(
                    data => Assert.That(JArray.FromObject(data)[0]["label"].ToString(), Is.EqualTo("San Diego")));
        }
    }
}

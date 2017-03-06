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
            var leads = new List<Lead>
            {
                new Lead {LeadSource = new LeadSource {Name = "Downtown"} },
                new Lead {LeadSource = new LeadSource {Name = "Uptown"} }
            };

            var mockRepository = new Mock<IRepository<Lead>>();
            mockRepository.Setup(x => x.All()).Returns(leads);

            var controller = new StatisticsController(mockRepository.Object);
            controller.WithCallTo(c => c.LeadsBySource())
                .ShouldReturnJson(data =>
                {
                    var firstLead = JArray.FromObject(data).First;
                    Assert.That(firstLead.label.ToString(), Is.EqualTo("Downtown"));
                });
        }

        [Test]
        public void LeadsBySource_returns_leads_grouped_by_city()
        {
            var leads = new List<Lead>
            {
                new Lead {City = "San Diego"},
                new Lead {City = "San Francisco"}
            };

            var mockRepository = new Mock<IRepository<Lead>>();
            mockRepository.Setup(x => x.All()).Returns(leads);

            var controller = new StatisticsController(mockRepository.Object);
            controller.WithCallTo(c => c.LeadsByCity())
                .ShouldReturnJson(data =>
                {
                    var firstLead = JArray.FromObject(data).First;
                    Assert.That(firstLead.label.ToString(), Is.EqualTo("San Diego"));
                });
        }
    }
}

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
                    System.Type type = data.GetType();
                    var dataset = type.GetProperty("datasets").GetValue(data, null);
                    System.Type typeDataset = dataset[0].GetType();
                    Assert.That((string[])type.GetProperty("labels").GetValue(data, null), Is.EqualTo(new[] { "Downtown", "Uptown" }));
                    Assert.That((int[])typeDataset.GetProperty("data").GetValue(dataset[0], null), Is.EqualTo(new[] { 1, 1 }));
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
                    System.Type type = data.GetType();
                    var dataset = type.GetProperty("datasets").GetValue(data, null);
                    System.Type typeDataset = dataset[0].GetType();
                    Assert.That((string[])type.GetProperty("labels").GetValue(data, null), Is.EqualTo(new[] { "San Diego", "San Francisco" }));
                    Assert.That((int[])typeDataset.GetProperty("data").GetValue(dataset[0], null), Is.EqualTo(new[] { 1, 1 }));
                });
        }
    }
}

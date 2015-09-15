using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CallTracking.Web.Controllers;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using Moq;
using NUnit.Framework;

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
            var result = controller.LeadsBySource();

            var sb = new StringBuilder();
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(s => s.Write(It.IsAny<string>())).Callback<string>(c => sb.Append(c));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(x => x.HttpContext.Response).Returns(mockResponse.Object);

            result.ExecuteResult(mockControllerContext.Object);
            Assert.AreEqual(@"[{""label"":""Downtown"",""value"":2}]", sb.ToString());
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
            var result = controller.LeadsByCity();

            var sb = new StringBuilder();
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(s => s.Write(It.IsAny<string>())).Callback<string>(c => sb.Append(c));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(x => x.HttpContext.Response).Returns(mockResponse.Object);

            result.ExecuteResult(mockControllerContext.Object);
            Assert.AreEqual(@"[{""label"":""San Diego"",""value"":2},{""label"":""Modesto"",""value"":1}]", sb.ToString());
        }
    }
}

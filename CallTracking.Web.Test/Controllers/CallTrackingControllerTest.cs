using System;
using System.Xml.XPath;
using CallTracking.Web.Controllers;
using CallTracking.Web.Models;
using CallTracking.Web.Models.Repository;
using FluentMvcTesting.Extensions;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace CallTracking.Web.Test.Controllers
{
    public class CallTrackingControllerTest
    {
        private const string ForwardingNumber = "+12568417333";

        [Test]
        public void ForwardCall_responds_with_Dial_verb()
        {
            var mockLeadSourcesRepository = new Mock<IRepository<LeadSource>>();
            var mockLeadsRepository = new Mock<IRepository<Lead>>();
            mockLeadSourcesRepository.Setup(x => x.FirstOrDefault(It.IsAny<Func<LeadSource, bool>>()))
                .Returns(new LeadSource {ForwardingNumber = ForwardingNumber});

            var controller = new CallTrackingController(
                mockLeadSourcesRepository.Object, mockLeadsRepository.Object);

            controller.WithCallTo(c => c.ForwardCall("called", "caller", "city", "state"))
                .ShouldReturnXmlResult(data =>
                {
                    Assert.That(data.XPathSelectElement("/Response/Dial").Value, Is.EqualTo(ForwardingNumber));
                });

            mockLeadsRepository.Verify(r => r.Create(It.IsAny<Lead>()), Times.Once);
        }
    }
}

using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CallTracking.Web.Controllers;
using CallTracking.Web.Domain.Twilio;
using Moq;
using NUnit.Framework;
using Twilio;

namespace CallTracking.Web.Test.Controllers
{
    public class AvailablePhoneNumbersControllerTest
    {
        [Test]
        public void Index_returns_a_list_of_phone_numbers()
        {
            var mockRestClient = new Mock<IRestClient>();
            var phoneNumbers = new List<AvailablePhoneNumber>();
            mockRestClient.Setup(x => x.SearchPhoneNumbers(It.IsAny<string>())).Returns(phoneNumbers);
            var controller = GetAvailablePhoneNumbersController(mockRestClient.Object);

            var result = controller.Index("415") as ViewResult;

            Assert.That(result.ViewData.Model, Is.EqualTo(phoneNumbers));
        }

        private static AvailablePhoneNumbersController GetAvailablePhoneNumbersController(IRestClient client)
        {
            var controller = new AvailablePhoneNumbersController(client);

            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return controller;
        }
    }
}

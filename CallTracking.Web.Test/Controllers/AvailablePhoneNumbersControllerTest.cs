using System.Collections.Generic;
using CallTracking.Web.Controllers;
using CallTracking.Web.Domain.Twilio;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using Twilio.Rest.Api.V2010.Account.AvailablePhoneNumberCountry;

namespace CallTracking.Web.Test.Controllers
{
    public class AvailablePhoneNumbersControllerTest
    {
        [Test]
        public void Index_returns_a_list_of_phone_numbers()
        {
            var localNumbers = new List<LocalResource>
            {
                LocalResource.FromJson("{\"address_requirements\": \"none\"}")
            };

            var mockRestClient = new Mock<IRestClient>();
            mockRestClient
                .Setup(c => c.SearchPhoneNumbersAsync(It.IsAny<string>()))
                .ReturnsAsync(localNumbers);

            var controller = new AvailablePhoneNumbersController(mockRestClient.Object);
            controller.WithCallTo(c => c.Index("415"))
                .ShouldRenderDefaultView()
                .WithModel(localNumbers);
        }
    }
}

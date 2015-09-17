using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RestSharp;
using Twilio;

namespace CallTracking.Web.Test.Domain.Twilio
{
    public class RestClientTest
    {
        private Mock<TwilioRestClient> _mockClient;

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<TwilioRestClient>("AccountSid", "AuthToken") { CallBase = true };
        }

        [Test]
        public void SearchPhoneNumbers()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(trc => trc.Execute<AvailablePhoneNumberResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new AvailablePhoneNumberResult());
            new Web.Domain.Twilio.RestClient(_mockClient.Object).SearchPhoneNumbers();

            _mockClient.Verify(trc => trc.Execute<AvailablePhoneNumberResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }

        [Test]
        public void PurchasePhoneNumber()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(trc => trc.Execute<IncomingPhoneNumber>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new IncomingPhoneNumber());

            new Web.Domain.Twilio.RestClient(_mockClient.Object).PurchasePhoneNumber("+14152339867", "ApplicationSid");

            _mockClient.Verify(trc => trc.Execute<IncomingPhoneNumber>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }

        [Test]
        public void GetApplicationSid_verifies_an_application_result_is_returned_when_listing_aplications()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(trc => trc.Execute<ApplicationResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>((request) => savedRequest = request)
                .Returns(new ApplicationResult {Applications = new List<Application>()});

            new Web.Domain.Twilio.RestClient(_mockClient.Object).GetApplicationSid();

            _mockClient.Verify(trc => trc.Execute<ApplicationResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }

        [Test]
        public void GetApplicationSid_verifies_an_application_is_created_if_there_are_no_applications()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(trc => trc.ListApplications(It.IsAny<string>(), null, null))
                .Returns(new ApplicationResult { Applications = new List<Application>() });

            _mockClient.Setup(trc => trc.Execute<Application>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>((request) => savedRequest = request)
                .Returns(new Application());

            new Web.Domain.Twilio.RestClient(_mockClient.Object).GetApplicationSid();

            _mockClient.Verify(trc => trc.Execute<Application>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.That(savedRequest, Is.Not.Null);
        }
    }
}

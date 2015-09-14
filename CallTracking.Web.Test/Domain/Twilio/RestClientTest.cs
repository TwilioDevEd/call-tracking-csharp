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
            Assert.IsNotNull(savedRequest);
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
            Assert.IsNotNull(savedRequest);
        }
    }
}

using System.Web.Configuration;

namespace CallTracking.Web.Domain.Twilio
{
    public static class Credentials
    {
        public static string TwilioAccountSid = WebConfigurationManager.AppSettings["TwilioAccountSid"];
        public static string TwilioAuthToken = WebConfigurationManager.AppSettings["TwilioAuthToken"];
        public static string TwiMLApplicationSid = WebConfigurationManager.AppSettings["TwiMLApplicationSid"];
    }
}
using System.Web.Configuration;

namespace CallTracking.Web.Domain.Twilio
{
    public static class Credentials
    {
        public static string AccountSid = WebConfigurationManager.AppSettings["AccountSid"];
        public static string AuthToken = WebConfigurationManager.AppSettings["AuthToken"];
        public static string ApplicationSid = WebConfigurationManager.AppSettings["ApplicationSid"];
    }
}
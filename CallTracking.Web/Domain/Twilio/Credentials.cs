using System.Web.Configuration;

namespace CallTracking.Web.Domain.Twilio
{
    public static class Credentials
    {
        public static string TwilioAccountSid = WebConfigurationManager.AppSettings["TwilioAccountSid"];
        public static string TwilioAuthToken = WebConfigurationManager.AppSettings["TwilioAuthToken"];
        public static string TwiMLApplicationSid {
            get
            {
                var twiMLApplicationSid = WebConfigurationManager.AppSettings["TwiMLApplicationSid"];
                switch (twiMLApplicationSid)
                {
                    case "TWIML_APPLICATION_SID":
                    case "":
                        return null;
                    default:
                        return twiMLApplicationSid;
                }
            }
        }
    }
}
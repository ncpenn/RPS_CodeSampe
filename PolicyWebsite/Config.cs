using System.Configuration;

namespace Website.PolicyWebsite
{
    public static class Config
    {
        static Config()
        {
            PolicyServiceUrl = ConfigurationManager.AppSettings["PolicyServiceUrl"];
            PolicyServiceSecret = ConfigurationManager.AppSettings["PolicyServiceSecret"];
        }

        public static string PolicyServiceUrl { get; }
        public static string PolicyServiceSecret { get; internal set; }
    }
}
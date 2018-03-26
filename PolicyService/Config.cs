using System.Configuration;

namespace Service.PolicyService
{
    internal static class Config
    {
        static Config()
        {
            PolicyServiceSecret = ConfigurationManager.AppSettings["PolicyServiceSecret"];
        }

        public static string PolicyServiceSecret { get; internal set; }
    }
}
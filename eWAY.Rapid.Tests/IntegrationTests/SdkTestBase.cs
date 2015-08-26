using System.Configuration;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    public abstract class SdkTestBase
    {
        public static string PASSWORD = ConfigurationManager.AppSettings["PASSWORD"];
        public static string APIKEY = ConfigurationManager.AppSettings["APIKEY"];
        public static string ENDPOINT = ConfigurationManager.AppSettings["ENDPOINT"];

        protected IRapidClient CreateRapidApiClient()
        {
            return RapidClientFactory.NewRapidClient(APIKEY, PASSWORD, ENDPOINT);
        }
    }
}

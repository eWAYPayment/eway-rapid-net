using System.Configuration;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    public abstract class SdkTestBase
    {
        public static string PASSWORD = ConfigurationManager.AppSettings["PASSWORD"];
        public static string APIKEY = ConfigurationManager.AppSettings["APIKEY"];
        public static string ENDPOINT = ConfigurationManager.AppSettings["ENDPOINT"];
        public static int APIVERSION = int.Parse(ConfigurationManager.AppSettings["APIVERSION"]);

        protected IRapidClient CreateRapidApiClient()
        {
            var client = RapidClientFactory.NewRapidClient(APIKEY, PASSWORD, ENDPOINT);
            client.SetVersion(GetVersion());
            return client;
        }

        protected int GetVersion()
        {
            string version = System.Environment.GetEnvironmentVariable("APIVERSION");
            int v;
            if (version != null && int.TryParse(version, out v))
            {
                return v;
            }
            return APIVERSION;
        }
    }
}

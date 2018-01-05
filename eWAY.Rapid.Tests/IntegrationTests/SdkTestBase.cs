using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    public abstract class SdkTestBase
    {
        protected SdkTestBase()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            PASSWORD = configuration["PASSWORD"];
            APIKEY = configuration["APIKEY"];
            ENDPOINT = configuration["ENDPOINT"];
            APIVERSION = Convert.ToInt32(configuration["APIVERSION"]);
        }

        public static string PASSWORD;
        public static string APIKEY;
        public static string ENDPOINT;
        public static int APIVERSION;

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

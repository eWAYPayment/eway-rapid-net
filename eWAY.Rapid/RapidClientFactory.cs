using System.Globalization;
using System.Reflection;
using System.Resources;
using eWAY.Rapid.Internals;
using eWAY.Rapid.Internals.Services;

namespace eWAY.Rapid
{
    using System.Net;

    /// <summary>
    /// Factory class to create an instance of IRapidSdkClient
    /// </summary>
    public static class RapidClientFactory
    {
        /// <summary>
        /// Static method to create a new Rapid SDK Client configured to communicate with eWAY's Rapid API
        /// </summary>
        /// <param name="apiKey">Rapid API Key</param>
        /// <param name="password">Password for the API Key</param>
        /// <param name="rapidEndpoint">Possible values ("Production", "Sandbox", or a URL) Production and sandbox will default to the Global Rapid API Endpoints.</param>
        /// <param name="securityProtocol">Security protocol to connect to the EWay API. Default is self negotiated by .NET.</param>
        /// <returns>Native class/object that can be used to create and access business objects such as customers and transactions. </returns>
        public static IRapidClient NewRapidClient(string apiKey, string password, string rapidEndpoint, SecurityProtocolType? securityProtocol = null)
        {
            return new RapidClient(new RapidService(apiKey, password, rapidEndpoint, securityProtocol));
        }

        /// <summary>
        /// This static/utility method will provide a message suitable for display to a user corresponding to a given Rapid Code/language.
        /// </summary>
        /// <param name="errorCode">Rapid API Error Code e.g. "V6023" </param>
        /// <param name="language">Language Code, e.g. "EN" (default) or "ES"</param>
        /// <returns>String with a description for the given code in the specified language</returns>
        public static string UserDisplayMessage(string errorCode, string language)
        {
            ResourceManager rm = new ResourceManager("eWAY.Rapid.Resources.ErrorMessages", Assembly.GetExecutingAssembly());

            string result = null;

            try
            {
                var cultureInfo = new CultureInfo(language);
                return result = rm.GetString(errorCode, cultureInfo);
            }
            catch (CultureNotFoundException)
            {
                var cultureInfo = new CultureInfo(SystemConstants.DEFAULT_LANGUAGE_CODE);
                return result = rm.GetString(errorCode, cultureInfo);
            }
            catch (MissingManifestResourceException)
            {
                var cultureInfo = new CultureInfo(SystemConstants.DEFAULT_LANGUAGE_CODE);
                try
                {
                    return result = rm.GetString(errorCode, cultureInfo);
                }
                catch (MissingManifestResourceException)
                {
                    return SystemConstants.INVALID_ERROR_CODE_MESSAGE;
                }
            }
        }
    }
}

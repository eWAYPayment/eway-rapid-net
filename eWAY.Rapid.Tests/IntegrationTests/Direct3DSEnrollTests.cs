using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class Direct3DSEnrollTests : SdkTestBase
    {
        private IRapidClient _client;
        private Direct3DSEnrollRequest _request;

        [TestInitialize]
        public void Initial()
        {
            _client = CreateRapidApiClient();
            _request = TestUtil.CreateEnrollRequest();
        }

        [TestMethod]
        public void Enroll_Returns_ValidResponse()
        {
            var response = _client.Direct3DSEnroll(_request);

            Assert.IsFalse(string.IsNullOrEmpty(response.Default3dsUrl));
            Assert.AreNotEqual(0, response.TraceId);
            Assert.IsFalse(string.IsNullOrEmpty(response.AccessCode));
            Assert.IsNull(response.Errors);
        }

        [TestMethod]
        public void Enroll_Returns_ErrorResponse()
        {
            var request = TestUtil.CreateEnrollRequest();
            //Send no postal code will cause error V6068
            request.Customer.Address.PostalCode = "";

            var response = _client.Direct3DSEnroll(request);

            Assert.IsTrue(string.IsNullOrEmpty(response.Default3dsUrl));
            Assert.AreEqual(0, response.TraceId);
            Assert.IsTrue(string.IsNullOrEmpty(response.AccessCode));
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual("V6068", response.Errors[0]);
        }
    }
}

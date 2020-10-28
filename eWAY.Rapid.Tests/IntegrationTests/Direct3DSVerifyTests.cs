using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class Direct3DSVerifyTests : SdkTestBase
    {
        private IRapidClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = CreateRapidApiClient();
        }

        [TestMethod]
        public void Verify_Returns_Valid_Response()
        {
            var request = Generate3DSVerifyRequest();
            var response = _client.Direct3DSVerify(request);

            Assert.AreNotEqual(0, response.TraceId);
            Assert.IsNotNull(response.AccessCode);
            Assert.IsNotNull(response.ThreeDSecureAuth);
            Assert.IsNull(response.Errors);
        }

        public void Verify_Returns_Errors_Invalid_Response()
        {
            var request = Generate3DSVerifyRequest();
            request.TraceId = 0;
            var response = _client.Direct3DSVerify(request);

            Assert.AreEqual(0, response.TraceId);
            Assert.IsNull(response.AccessCode);
            Assert.IsNull(response.ThreeDSecureAuth);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual("D4417", response.Errors[0]);
        }

        private Direct3DSVerifyRequest Generate3DSVerifyRequest()
        {
            var enrollResponse = _client.Direct3DSEnroll(TestUtil.CreateEnrollRequest());
            return new Direct3DSVerifyRequest()
            {
                TraceId = enrollResponse.TraceId,
                AccessCode = enrollResponse.AccessCode
            };
        }
    }
}

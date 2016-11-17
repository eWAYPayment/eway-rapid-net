using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Internals.Services;
using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BaseResponse = eWAY.Rapid.Internals.Response.BaseResponse;

namespace eWAY.Rapid.Tests.MappingTests
{
    [TestClass]
    public class MappingTests
    {
        readonly IMappingService _mappingService = new MappingService();

        [TestMethod]
        public void Transaction_To_DirectPaymentRequest_Test()
        { 
            var source = TestUtil.CreateTransaction();
            var dest = _mappingService.Map<Transaction, DirectPaymentRequest>(source);
            Assert.AreEqual(source.CustomerIP, dest.CustomerIP);
        }

        [TestMethod]
        public void ErrorMapping_NoError_Test()
        {
            var source = new BaseResponse()
            {
                Errors = null
            };
            var dest = _mappingService.Map<BaseResponse,
                Models.BaseResponse>(source);

            Assert.IsNull(dest.Errors);
        }

        [TestMethod]
        public void ErrorMapping_Test()
        {
            var source = new BaseResponse()
            {
                Errors = "D4401,D4403,D4404"
            };

            var dest = _mappingService.Map<BaseResponse,
                Models.BaseResponse>(source);
            Assert.AreEqual(dest.Errors.Count, 3);
            Assert.AreEqual(dest.Errors[0], "D4401");
            Assert.AreEqual(dest.Errors[1], "D4403");
            Assert.AreEqual(dest.Errors[2], "D4404");
        }

        [TestMethod]
        public void ErrorMapping_Inheritance_Test()
        {
            var source = TestUtil.CreateDirectPaymentResponse();
            source.Errors = "D4401,D4403,D4404";

            var dest = _mappingService.Map<DirectPaymentResponse, CreateTransactionResponse>(source);
            Assert.AreEqual(dest.Errors.Count, 3);
            Assert.AreEqual(dest.Errors[0], "D4401");
            Assert.AreEqual(dest.Errors[1], "D4403");
            Assert.AreEqual(dest.Errors[2], "D4404");
        }


        [TestMethod]
        public void DirectPaymentResponse_To_CreateTransactionResponse_Test()
        {
            var source = TestUtil.CreateDirectPaymentResponse();
            var dest = _mappingService.Map<DirectPaymentResponse, CreateTransactionResponse>(source);
            Assert.AreEqual(source.TransactionStatus, dest.TransactionStatus.Status);
        }
    }
}

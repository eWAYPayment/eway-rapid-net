using eWAY.Rapid.Enums;
using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class PreAuthTests : SdkTestBase
    {
        [TestMethod]
        public void PreAuth_CapturePayment_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction(false);
            var preAuthTransaction = client.Create(PaymentMethod.Direct, transaction);
            //Act
            var preAuthRequest = new CapturePaymentRequest()
            {
                Payment = new Payment()
                {
                    CurrencyCode = preAuthTransaction.Transaction.PaymentDetails.CurrencyCode,
                    InvoiceDescription = preAuthTransaction.Transaction.PaymentDetails.InvoiceDescription,
                    InvoiceNumber = preAuthTransaction.Transaction.PaymentDetails.InvoiceNumber,
                    InvoiceReference = preAuthTransaction.Transaction.PaymentDetails.InvoiceReference,
                    TotalAmount = preAuthTransaction.Transaction.PaymentDetails.TotalAmount
                },
                TransactionId = preAuthTransaction.TransactionStatus.TransactionID.ToString()
            };
            var preAuthResponse = client.CapturePayment(preAuthRequest);
            //Assert
            Assert.IsNotNull(preAuthResponse);
            Assert.IsTrue(preAuthResponse.TransactionStatus);
            Assert.IsNotNull(preAuthResponse.ResponseMessage);
            Assert.IsNotNull(preAuthResponse.ResponseCode);
            Assert.IsNotNull(preAuthResponse.TransactionID);
        }

        [TestMethod]
        public void PreAuth_CancelAuthorisation_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction(false);
            var preAuthTransaction = client.Create(PaymentMethod.Direct, transaction);
            //Act
            var preAuthRequest = new CancelAuthorisationRequest()
            {
                TransactionId = preAuthTransaction.TransactionStatus.TransactionID.ToString()
            };

            var preAuthResponse = client.CancelAuthorisation(preAuthRequest);
            //Assert
            Assert.IsNotNull(preAuthResponse);
            Assert.IsTrue(preAuthResponse.TransactionStatus);
            Assert.IsNotNull(preAuthResponse.ResponseMessage);
            Assert.IsNotNull(preAuthResponse.ResponseCode);
            Assert.IsNotNull(preAuthResponse.TransactionID);
        }
    }
}

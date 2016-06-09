using System;
using System.Linq;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Internals.Services;
using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class QueryTransactionTests : SdkTestBase
    {
        [TestMethod]
        public void QueryTransaction_ByTransactionId_Test()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction();

            //Act
            var response = client.Create(PaymentMethod.Direct, transaction);
            var filter = new TransactionFilter() {TransactionID = response.TransactionStatus.TransactionID};
            var queryResponse = client.QueryTransaction(filter);
            var queryResponse2 = client.QueryTransaction(response.TransactionStatus.TransactionID);
            //Assert
            Assert.IsNotNull(queryResponse);
            Assert.IsNotNull(queryResponse2);
            Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse.TransactionStatus.TransactionID);
            Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse2.TransactionStatus.TransactionID);
            Assert.AreEqual(response.TransactionStatus.Total, queryResponse2.TransactionStatus.Total);
            //TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
            //    queryResponse.Transaction.Customer);
            //TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Transaction.Customer,
            //    queryResponse.Transaction.Customer);
            //TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
            //    queryResponse2.Transaction.Customer);
            //TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Transaction.Customer,
            //    queryResponse2.Transaction.Customer);
        }
        [TestMethod]
        public void QueryTransaction_ByAccessCode_Test()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction();

            //Act
            var response = client.Create(PaymentMethod.TransparentRedirect, transaction);
            var filter = new TransactionFilter() {AccessCode = response.AccessCode};
            var queryResponse = client.QueryTransaction(filter);
            var queryResponse2 = client.QueryTransaction(response.AccessCode);
            //Assert
            Assert.IsNotNull(queryResponse);
            Assert.IsNotNull(queryResponse2);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                queryResponse.Transaction.Customer);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                queryResponse2.Transaction.Customer);
        }
        [TestMethod]
        public void QueryTransaction_ByInvoiceRef_Test()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction();
            var r = new Random();
            var randomInvoiceRef = r.Next(100000, 999999);
            transaction.PaymentDetails.InvoiceReference = randomInvoiceRef.ToString();
            //Act
            var response = client.Create(PaymentMethod.Direct, transaction);
            var filter = new TransactionFilter()
            {
                InvoiceReference = response.Transaction.PaymentDetails.InvoiceReference
            };
            var queryResponse = client.QueryTransaction(filter);
            var queryResponse2 = client.QueryInvoiceRef(response.Transaction.PaymentDetails.InvoiceReference);
            //Assert
            Assert.IsNotNull(queryResponse);
            Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse.TransactionStatus.TransactionID);
            Assert.IsNotNull(queryResponse2);
            Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse2.TransactionStatus.TransactionID);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                queryResponse.Transaction.Customer);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                queryResponse2.Transaction.Customer);
        }
        [TestMethod]
        public void QueryTransaction_ByInvoiceNumber_Test()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction();
            var r = new Random();
            var randomInvoiceNumber = r.Next(10000, 99999);
            transaction.PaymentDetails.InvoiceNumber = "Inv " + randomInvoiceNumber;
            //Act
            var response = client.Create(PaymentMethod.Direct, transaction);
            var filter = new TransactionFilter()
            {
                InvoiceNumber = response.Transaction.PaymentDetails.InvoiceNumber
            };
            var queryResponse = client.QueryTransaction(filter);
            var queryResponse2 = client.QueryInvoiceNumber(response.Transaction.PaymentDetails.InvoiceNumber);
            //Assert
            Assert.IsNotNull(queryResponse);
            Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse.TransactionStatus.TransactionID);
            Assert.IsNotNull(queryResponse2);
            Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse2.TransactionStatus.TransactionID);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                queryResponse.Transaction.Customer);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                queryResponse2.Transaction.Customer);
        }

        [TestMethod]
        public void QueryTransaction_InvalidInputData_ReturnVariousErrors()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var filter = new TransactionFilter()
            {
                TransactionID = -1
            };
            //Act
            var queryByIdResponse = client.QueryTransaction(filter);
            //Assert
            Assert.IsNotNull(queryByIdResponse.Errors);
            Assert.AreEqual(queryByIdResponse.Errors.FirstOrDefault(), "S9995");

            //Arrange
            filter = new TransactionFilter()
            {
                AccessCode = "leRandomAccessCode"
            };
            //Act
            var queryByAccessCodeResponse = client.QueryTransaction(filter);
            //Assert
            Assert.IsNull(queryByAccessCodeResponse.Transaction);

            //Arrange
            filter = new TransactionFilter()
            {
                InvoiceNumber = "leRandomInvoiceNumber"
            };
            //Act
            var queryByInvoiceNumberResponse = client.QueryTransaction(filter);
            //Assert
            Assert.IsNotNull(queryByInvoiceNumberResponse.Errors);
            Assert.AreEqual(queryByInvoiceNumberResponse.Errors.FirstOrDefault(), "V6171");

            //Arrange
            filter = new TransactionFilter()
            {
                InvoiceReference = "leRandomInvoiceReference"
            };
            //Act
            var queryByInvoiceRefResponse = client.QueryTransaction(filter);
            //Assert
            Assert.IsNotNull(queryByInvoiceRefResponse.Errors);
            Assert.AreEqual(queryByInvoiceRefResponse.Errors.FirstOrDefault(), "V6171");
        }

        [TestMethod]
        public void QueryTransaction_Rapidv40_Test()
        {
            if (GetVersion() > 31)
            {
                var client = CreateRapidApiClient();
                //Arrange
                var transaction = TestUtil.CreateTransaction();

                //Act
                var response = client.Create(PaymentMethod.Direct, transaction);
                var filter = new TransactionFilter() { TransactionID = response.TransactionStatus.TransactionID };
                var queryResponse = client.QueryTransaction(filter);
                var queryResponse2 = client.QueryTransaction(response.TransactionStatus.TransactionID);
                //Assert
                Assert.IsNotNull(queryResponse);
                Assert.IsNotNull(queryResponse2);
                Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse.TransactionStatus.TransactionID);
                Assert.AreEqual(response.TransactionStatus.TransactionID, queryResponse2.TransactionStatus.TransactionID);
                Assert.AreEqual(response.TransactionStatus.Total, queryResponse2.TransactionStatus.Total);

                Assert.AreEqual("036", queryResponse2.Transaction.CurrencyCode);
                Assert.AreEqual(response.TransactionStatus.Total, queryResponse2.Transaction.MaxRefund);
            } else
            {
                Assert.Inconclusive();
            }

        }
    }
}

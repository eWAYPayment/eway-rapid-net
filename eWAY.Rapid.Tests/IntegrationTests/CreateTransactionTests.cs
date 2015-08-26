using System.Linq;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class CreateTransactionTests: SdkTestBase
    {
        [TestMethod]
        public void Transaction_CreateTransactionDirect_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction();

            //Act
            var response = client.Create(PaymentMethod.Direct, transaction);

            //Assert
            Assert.IsNull(response.Errors);
            Assert.IsNotNull(response.Transaction);
            Assert.IsNotNull(response.TransactionStatus);
            Assert.IsNotNull(response.TransactionStatus.Status);
            Assert.IsTrue(response.TransactionStatus.Status.Value);
            Assert.IsTrue(response.TransactionStatus.TransactionID > 0);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                transaction.Customer);
            TestUtil.AssertReturnedCustomerData_VerifyCardDetailsAreEqual(response.Transaction.Customer,
                transaction.Customer);
            TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Transaction.Customer,
                transaction.Customer);
        }

        [TestMethod]
        public void Transaction_CreateTransactionTransparentRedirect_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction();

            //Act
            var response = client.Create(PaymentMethod.TransparentRedirect, transaction);

            //Assert
            Assert.IsNull(response.Errors);
            Assert.IsNotNull(response.AccessCode);
            Assert.IsNotNull(response.FormActionUrl);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                transaction.Customer);
            TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Transaction.Customer,
                transaction.Customer);
        }

        [TestMethod]
        public void Transaction_CreateTransactionResponsiveShared_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction();

            //Act
            var response = client.Create(PaymentMethod.ResponsiveShared, transaction);

            //Assert
            Assert.IsNull(response.Errors);
            Assert.IsNotNull(response.AccessCode);
            Assert.IsNotNull(response.FormActionUrl);
            Assert.IsNotNull(response.SharedPaymentUrl);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Transaction.Customer,
                transaction.Customer);
            TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Transaction.Customer,
                transaction.Customer);
        }

        [TestMethod]
        public void Transaction_CreateTransactionDirect_InvalidInputData_ReturnVariousErrors()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction(true);
            transaction.Customer.CardDetails.Number = "-1";
            //Act
            var response1 = client.Create(PaymentMethod.Direct, transaction);
            //Assert
            Assert.IsNotNull(response1.Errors);
            Assert.AreEqual(response1.Errors.FirstOrDefault(), "V6110");
            //Arrange
            transaction = TestUtil.CreateTransaction(true);
            transaction.PaymentDetails.TotalAmount = -1;
            //Act
            var response2 = client.Create(PaymentMethod.Direct, transaction);
            //Assert
            Assert.IsNotNull(response2.Errors);
            Assert.AreEqual(response2.Errors.FirstOrDefault(), "V6011");
        }

        [TestMethod]
        public void Transaction_CreateTransactionTransparentRedirect_InvalidInputData_ReturnVariousErrors()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction(true);
            transaction.PaymentDetails.TotalAmount = 0;
            //Act
            var response1 = client.Create(PaymentMethod.TransparentRedirect, transaction);
            //Assert
            Assert.IsNotNull(response1.Errors);
            Assert.AreEqual(response1.Errors.FirstOrDefault(), "V6011");
            //Arrange
            transaction = TestUtil.CreateTransaction(true);
            transaction.RedirectURL = "anInvalidRedirectUrl";
            //Act
            var response2 = client.Create(PaymentMethod.TransparentRedirect, transaction);
            //Assert
            Assert.IsNotNull(response2.Errors);
            Assert.AreEqual(response2.Errors.FirstOrDefault(), "V6059");
        }

        [TestMethod]
        public void Transaction_CreateTransactionResponsiveShared_InvalidInputData_ReturnVariousErrors()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var transaction = TestUtil.CreateTransaction(true);
            transaction.PaymentDetails.TotalAmount = 0;
            //Act
            var response1 = client.Create(PaymentMethod.TransparentRedirect, transaction);
            //Assert
            Assert.IsNotNull(response1.Errors);
            Assert.AreEqual(response1.Errors.FirstOrDefault(), "V6011");
            //Arrange
            transaction = TestUtil.CreateTransaction(true);
            transaction.RedirectURL = "anInvalidRedirectUrl";
            //Act
            var response2 = client.Create(PaymentMethod.TransparentRedirect, transaction);
            //Assert
            Assert.IsNotNull(response2.Errors);
            Assert.AreEqual(response2.Errors.FirstOrDefault(), "V6059");
        }
    }
}

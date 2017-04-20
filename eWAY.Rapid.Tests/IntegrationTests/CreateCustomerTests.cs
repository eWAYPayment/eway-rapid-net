using System.Linq;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Internal = eWAY.Rapid.Internals.Models;
namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class CreateCustomerTests : SdkTestBase
    {
        [TestMethod]
        public void Customer_CreateCustomerDirect_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();

            //Act
            var response = client.Create(PaymentMethod.Direct, customer);

            //Assert
            Assert.IsNull(response.Errors);
            Assert.IsNotNull(response.Customer);
            Assert.IsNotNull(response.Customer.TokenCustomerID);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Customer, customer);
            TestUtil.AssertReturnedCustomerData_VerifyCardDetailsAreEqual(response.Customer, customer);
            TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Customer, customer);
        }

        [TestMethod]
        public void Customer_CreateCustomerTransparentRedirect_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();

            //Act
            var response = client.Create(PaymentMethod.TransparentRedirect, customer);

            //Assert
            Assert.IsNull(response.Errors);
            Assert.IsNotNull(response.AccessCode);
            Assert.IsNotNull(response.FormActionUrl);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Customer, customer);
            TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Customer, customer);
        }

        [TestMethod]
        public void Customer_CreateCustomerResponsiveShared_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();
            customer.CancelURL = "http://www.eway.com.au";

            //Act
            var response = client.Create(PaymentMethod.ResponsiveShared, customer);

            //Assert
            Assert.IsNull(response.Errors);
            Assert.IsNotNull(response.AccessCode);
            Assert.IsNotNull(response.FormActionUrl);
            Assert.IsNotNull(response.SharedPaymentUrl);
            TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(response.Customer, customer);
            TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(response.Customer, customer);
        }

        [TestMethod]
        public void Customer_CreateCustomerDirect_InvalidInputData_VerifyReturnVariousErrors()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();
            customer.CardDetails.ExpiryYear = "-1";
            //Act
            var response1 = client.Create(PaymentMethod.Direct, customer);
            //Assert
            Assert.IsNotNull(response1.Errors);
            Assert.AreEqual(response1.Errors.FirstOrDefault(), "V6102");
            //Arrange
            customer = TestUtil.CreateCustomer();
            customer.Url = "anInvalidUrl";
            //Act
            var response2 = client.Create(PaymentMethod.Direct, customer);
            //Assert
            Assert.IsNotNull(response2.Errors);
            Assert.AreEqual(response2.Errors.FirstOrDefault(), "V6074");
        }

        [TestMethod]
        public void Customer_CreateCustomerTransparentRedirect_InvalidInputData_ReturnVariousErrors()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();
            customer.FirstName = null;
            //Act
            var response1 = client.Create(PaymentMethod.TransparentRedirect, customer);
            //Assert
            Assert.IsNotNull(response1.Errors);
            Assert.AreEqual(response1.Errors.FirstOrDefault(), "V6042");
            //Arrange
            customer = TestUtil.CreateCustomer();
            customer.RedirectURL = "anInvalidRedirectUrl";
            //Act
            var response2 = client.Create(PaymentMethod.TransparentRedirect, customer);
            //Assert
            Assert.IsNotNull(response2.Errors);
            Assert.AreEqual(response2.Errors.FirstOrDefault(), "V6059");
        }

        [TestMethod]
        public void Customer_CreateCustomerResponsiveShared_InvalidInputData_ReturnVariousErrors()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();
            customer.LastName = null;
            //Act
            var response1 = client.Create(PaymentMethod.ResponsiveShared, customer);
            //Assert
            Assert.IsNotNull(response1.Errors);
            Assert.AreEqual(response1.Errors.FirstOrDefault(), "V6043");
            //Arrange
            customer = TestUtil.CreateCustomer();
            customer.RedirectURL = "anInvalidRedirectUrl";
            //Act
            var response2 = client.Create(PaymentMethod.ResponsiveShared, customer);
            //Assert
            Assert.IsNotNull(response2.Errors);
            Assert.AreEqual(response2.Errors.FirstOrDefault(), "V6059");
        }
    }
}

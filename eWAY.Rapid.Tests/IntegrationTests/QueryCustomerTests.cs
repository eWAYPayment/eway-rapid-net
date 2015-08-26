using System.Linq;
using eWAY.Rapid.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class QueryCustomerTests : SdkTestBase
    {
        [TestMethod]
        public void QueryCustomer_ByCustomerTokenId_Test()
        {
            //Arrange
            var client = CreateRapidApiClient();
            var customer = TestUtil.CreateCustomer();

            //Act
            var createCustomerResponse = client.Create(PaymentMethod.Direct, customer);
            var customerId = long.Parse(createCustomerResponse.Customer.TokenCustomerID);
            var queryResponse = client.QueryCustomer(customerId);

            //Assert
            Assert.IsNotNull(queryResponse);
            Assert.AreEqual(createCustomerResponse.Customer.TokenCustomerID, queryResponse.Customers.First().TokenCustomerID);
            //TestUtil.AssertReturnedCustomerData_VerifyAddressAreEqual(createCustomerResponse.Customer,
                //queryResponse.Customers.First());
            TestUtil.AssertReturnedCustomerData_VerifyAllFieldsAreEqual(createCustomerResponse.Customer,
                queryResponse.Customers.First());
            TestUtil.AssertReturnedCustomerData_VerifyCardDetailsAreEqual(createCustomerResponse.Customer,
                queryResponse.Customers.First());
        }

        [TestMethod]
        public void QueryCustomer_ByCustomerTokenId_InvalidId_ReturnErrorV6040()
        {
            //Arrange
            var client = CreateRapidApiClient();
            
            //Act
            var customerId = -1;
            var queryResponse = client.QueryCustomer(customerId);

            //Assert
            Assert.IsNotNull(queryResponse.Errors);
            Assert.AreEqual(queryResponse.Errors.FirstOrDefault(), "V6040");
        }
    }
}

using System.Linq;
using eWAY.Rapid.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class UpdateCustomerTests: SdkTestBase
    {
        [TestMethod]
        public void Customer_UpdateCustomerDirect_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();
            var createResponse = client.Create(PaymentMethod.Direct, customer);
            customer.TokenCustomerID = createResponse.Customer.TokenCustomerID;
            //Act
            var updateResponse = client.UpdateCustomer(PaymentMethod.Direct, customer);

            //Assert
            Assert.AreEqual(createResponse.Customer.TokenCustomerID, updateResponse.Customer.TokenCustomerID);
        }

        [TestMethod]
        public void Customer_UpdateCustomerTransparentRedirect_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();
            var createResponse = client.Create(PaymentMethod.TransparentRedirect, customer);
            customer.TokenCustomerID = createResponse.Customer.TokenCustomerID;
            //Act
            var updateResponse = client.UpdateCustomer(PaymentMethod.Direct, customer);

            //Assert
            Assert.AreEqual(createResponse.Customer.TokenCustomerID, updateResponse.Customer.TokenCustomerID);
        }

        [TestMethod]
        public void Customer_UpdateCustomerResponsiveShared_ReturnValidData()
        {
            var client = CreateRapidApiClient();
            //Arrange
            var customer = TestUtil.CreateCustomer();
            var createResponse = client.Create(PaymentMethod.TransparentRedirect, customer);
            customer.TokenCustomerID = createResponse.Customer.TokenCustomerID;
            //Act
            var updateResponse = client.UpdateCustomer(PaymentMethod.ResponsiveShared, customer);

            //Assert
            Assert.AreEqual(createResponse.Customer.TokenCustomerID, updateResponse.Customer.TokenCustomerID);
        }
    }
}

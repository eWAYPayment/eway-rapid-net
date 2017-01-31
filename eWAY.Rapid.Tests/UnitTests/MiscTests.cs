using System.Collections.Generic;
using System.Linq;
using System.Net;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Internals;
using eWAY.Rapid.Internals.Services;
using eWAY.Rapid.Models;
using eWAY.Rapid.Tests.IntegrationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace eWAY.Rapid.Tests.UnitTests
{
    public interface IHttpWebRequestFactory
    {
        HttpWebRequest Create(string uri);
    }
    [TestClass]
    public class MiscTests: SdkTestBase
    {
        [TestMethod]
        public void AuthenticationError_Test()
        {
            //Arrange
            var wr = new Mock<HttpWebResponse>();
            wr.Setup(c => c.StatusCode).Returns(HttpStatusCode.Unauthorized);
            var request = new Mock<HttpWebRequest>();
            request.Setup(c => c.GetResponse()).Returns(wr.Object);
            var transaction = TestUtil.CreateTransaction();
            var mockClient = new Mock<RapidService>(APIKEY, PASSWORD, ENDPOINT, SecurityProtocolType.Tls12);
            var we = new WebException("MockException", null, WebExceptionStatus.ProtocolError, wr.Object);
            mockClient.Setup(x => x.GetWebResponse(It.IsAny<WebRequest>(), It.IsAny<string>())).Throws(we);
            var client = new RapidClient(mockClient.Object);
            //Act
            var response = client.Create(PaymentMethod.Direct, transaction);
            //Assert
            Assert.IsTrue(response.Errors[0] == RapidSystemErrorCode.AUTHENTICATION_ERROR);
        }

        [TestMethod]
        public void CommunicationError_Test()
        {
            //Arrange
            var wr = new Mock<HttpWebResponse>();
            wr.Setup(c => c.StatusCode).Returns(HttpStatusCode.ServiceUnavailable);
            var request = new Mock<HttpWebRequest>();
            request.Setup(c => c.GetResponse()).Returns(wr.Object);
            var transaction = TestUtil.CreateTransaction();
            var mockClient = new Mock<RapidService>(APIKEY, PASSWORD, ENDPOINT, SecurityProtocolType.Tls12);
            var we = new WebException("MockException", null, WebExceptionStatus.ProtocolError, wr.Object);
            mockClient.Setup(x => x.GetWebResponse(It.IsAny<WebRequest>(), It.IsAny<string>())).Throws(we);
            var client = new RapidClient(mockClient.Object);
            //Act
            var response = client.Create(PaymentMethod.Direct, transaction);
            //Assert
            Assert.IsTrue(response.Errors[0] == RapidSystemErrorCode.COMMUNICATION_ERROR);
        }

        [TestMethod]
        public void SdkInvalidStateErrors_Test()
        {
            //Arrange
            var mockClient = new Mock<IRapidService>();
            mockClient.Setup(x => x.IsValid()).Returns(false);
            mockClient.Setup(x => x.GetErrorCodes()).Returns(new List<string>(new[] { RapidSystemErrorCode.INVALID_ENDPOINT_ERROR }));
            var transaction = TestUtil.CreateTransaction();
            var customer = TestUtil.CreateCustomer();
            var client = new RapidClient(mockClient.Object);
            //Act
            var response1 = client.Create(PaymentMethod.Direct, transaction);
            var response2 = client.Create(PaymentMethod.Direct, customer);
            //Assert
            Assert.IsNotNull(response1.Errors);
            Assert.AreEqual(response1.Errors.First(), RapidSystemErrorCode.INVALID_ENDPOINT_ERROR);
            Assert.IsNotNull(response2.Errors);
            Assert.AreEqual(response2.Errors.First(), RapidSystemErrorCode.INVALID_ENDPOINT_ERROR);
        }

        [TestMethod]
        public void SdkInternalErrors_Test()
        {
            //Arrange
            var mockClient = new Mock<IRapidService>();
            mockClient.Setup(x => x.GetErrorCodes()).Returns(new List<string>(new[] { RapidSystemErrorCode.INTERNAL_SDK_ERROR }));
            var client = new RapidClient(mockClient.Object);
            var filter = new TransactionFilter()
            {
               TransactionID  = 123,
               AccessCode = "abc",
               InvoiceNumber = "123",
               InvoiceReference = "123"
            };
            //Act
            var response = client.QueryTransaction(filter);
            //Assert
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.First(), RapidSystemErrorCode.INTERNAL_SDK_ERROR);
        }

        [TestMethod]
        public void UserDisplayMessage_ReturnValidErrorMessage()
        {
            //Arrange
            var testMessage = "Invalid TransactionType, account not certified for eCome only MOTO or Recurring available";
            //Act
            var message = RapidClientFactory.UserDisplayMessage("V6010", "en");
            //Assert
            Assert.AreEqual(message, testMessage);
        }

        [TestMethod]
        public void UserDisplayMessage_ReturnInvalidErrorMessage()
        { 
            //Arrange
            var testMessage = SystemConstants.INVALID_ERROR_CODE_MESSAGE;
            //Act
            var message = RapidClientFactory.UserDisplayMessage("blahblah", "en");
            //Assert
            Assert.AreEqual(message, testMessage);
        }

        [TestMethod]
        public void UserDisplayMessage_ReturnDefaultEnglishLanguage()
        {
            //Arrange
            var testMessage = "Invalid TransactionType, account not certified for eCome only MOTO or Recurring available";
            //Act
            var message1 = RapidClientFactory.UserDisplayMessage("V6010", "de");
            var message2 = RapidClientFactory.UserDisplayMessage("V6010", "blahblah");
            //Assert
            Assert.AreEqual(message1, testMessage);
            Assert.AreEqual(message2, testMessage);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardDetails = eWAY.Rapid.Models.CardDetails;
using Customer = eWAY.Rapid.Models.Customer;
using LineItem = eWAY.Rapid.Models.LineItem;
using Payment = eWAY.Rapid.Internals.Models.Payment;
using Refund = eWAY.Rapid.Models.Refund;
using VerificationResult = eWAY.Rapid.Internals.Models.VerificationResult;

namespace eWAY.Rapid.Tests
{
    public static class TestUtil
    {
        //Third party Merchant
        internal static Transaction CreateTransaction(bool capture, string tokenId = null, bool createToken = false)
        {
            var result = new Transaction { Capture = capture, SaveCustomer = createToken };
            var cardDetails = new CardDetails() { Name = "John Smith", Number = "4444333322221111", ExpiryMonth = "12", ExpiryYear = "25", CVN = "123" };
            var address = new Address()
            {
                Street1 = "Level 5",
                Street2 = "369 Queen Street",
                City = "Sydney",
                State = "NSW",
                Country = "au",
                PostalCode = "2000",
            };
            result.Customer = new Customer()
            {
                Reference = "A12345",
                Title = "Mr.",
                FirstName = "John",
                LastName = "Smith",
                CompanyName = "Demo Shop 123",
                JobDescription = "Developer",
                Phone = "09 889 0986",
                Mobile = "09 889 6542",
                Email = "demo@example.org",
                Url = "http://www.ewaypayments.com",
                CardDetails = cardDetails,
                Address = address,
                Comments = "",
                Fax = "",
                TokenCustomerID = tokenId
            };

            var shippingAddress = new Address()
            {
                Street1 = "Level 5",
                Street2 = "369 Queen Street",
                City = "Sydney",
                State = "NSW",
                Country = "au",
                PostalCode = "2000"
            };

            result.ShippingDetails = new ShippingDetails()
            {
                ShippingAddress = shippingAddress,
                ShippingMethod = ShippingMethod.NextDay,
                Email = "demo@example.org",
                Fax = "",
                FirstName = "John",
                LastName = "Smith",
                Phone = "09 889 0986"
            };

            result.LineItems = new[] 
            { 
                new LineItem()
                {
                    SKU = "12345678901234567890", 
                    Description = "Item Description 1",
                    Quantity = 1,
                    UnitCost = 400
                },
                new LineItem()
                {
                    SKU = "123456789012", 
                    Description = "Item Description 2",
                    Quantity = 1,
                    UnitCost = 400,
                }
            }.ToList();

            result.Options = new[] { "Option1", "Option2" }.ToList();
            result.PaymentDetails = new PaymentDetails()
            {
                TotalAmount = 1000,
                InvoiceNumber = "Inv 21540",
                InvoiceDescription = "Individual Invoice Description",
                InvoiceReference = "513456",
                CurrencyCode = "AUD"
            };

            result.TransactionType = TransactionTypes.Purchase;
            result.RedirectURL = "http://www.eway.com.au";
            result.DeviceID = "D1234";
            result.PartnerID = "ID";
            result.CustomerIP = "127.0.0.1";
            return result;
        }

        internal static Transaction CreateTransaction()
        {
            return CreateTransaction(true);
        }

        internal static Customer CreateCustomer()
        {
            var customer = new Customer
            {
                Title = "Mr.",
                FirstName = "John",
                LastName = "Smith",
                Reference = "A12345",
                Address = new Address()
                {
                    Country = "au",
                    City = "Sydney",
                    PostalCode = "",
                    State = "NSW",
                    Street1 = "Level 5",
                    Street2 = "369 Queen Street"
                },
                CardDetails =
                    new CardDetails()
                    {
                        Name = "John Smith",
                        Number = "4444333322221111",
                        ExpiryMonth = "12",
                        ExpiryYear = "25",
                        CVN = "123"
                    },
                JobDescription = "Developer",
                RedirectURL = "http://www.eway.com.au",
                Comments = "empty comment",
                CompanyName = "Demo Shop 123",
                Fax = "",
                Mobile = "09 889 0986",
                Email = "demo@example.org",
                Phone = "09 889 6542",
                Url = "http://www.ewaypayments.com"
            };
            return customer;
        }

        internal static Refund CreateRefund(int transactionId)
        {
            var result = new Refund
            {
                PartnerID = "P123",
                DeviceID = "D1234",
                RefundDetails = new RefundDetails()
                {
                    TotalAmount = 100,
                    InvoiceNumber = "Inv 21540",
                    InvoiceDescription = "Individual Invoice Description",
                    InvoiceReference = "513456",
                    CurrencyCode = "AUD",
                    OriginalTransactionID = transactionId
                },
                Customer = CreateCustomer(),
                ShippingDetails = new ShippingDetails()
                {
                    ShippingMethod = ShippingMethod.NextDay,
                    FirstName = "John",
                    LastName = "Smith",
                    Phone = "09 889 0986",
                    ShippingAddress = new Address()
                    {
                        Street1 = "Level 5",
                        Street2 = "369 Queen Street",
                        City = "Sydney",
                        State = "NSW",
                        Country = "au",
                        PostalCode = "2000"
                    }
                },
                LineItems = new List<LineItem>()
                {
                    new LineItem()
                    {
                        SKU = "12345678901234567890",
                        Description = "Item Description 1",
                        Quantity = 1,
                        UnitCost = 400,
                        Tax = 100,
                        Total = 500
                    },
                    new LineItem()
                    {
                        SKU = "123456789012",
                        Description = "Item Description 2",
                        Quantity = 1,
                        UnitCost = 400,
                        Tax = 100,
                        Total = 500
                    }
                },
                Options = new List<string>()
                {
                    "Option1",
                    "Option2"
                }
            };
            result.Customer.CardDetails = new CardDetails()
            {
                ExpiryMonth = "12",
                ExpiryYear = "25"
            };
            return result;
        }

        internal static DirectPaymentResponse CreateDirectPaymentResponse()
        {
            return new DirectPaymentResponse()
            {
                AuthorisationCode = "747774",
                ResponseCode = "00",
                ResponseMessage = "A2000",
                TransactionID = 11735855,
                TransactionStatus = true,
                TransactionType = "Purchase",
                BeagleScore = 0,
                Verification = new VerificationResult()
                {
                    CVN = 0,
                    Address = 0,
                    Email = 0,
                    Mobile = 0,
                    Phone = 0
                },
                Customer = new DirectTokenCustomer()
                {
                    CardDetails = new Internals.Models.CardDetails()
                    {
                        Number = "444433XXXXXX1111",
                        Name = "John Smith",
                        ExpiryMonth = "12",
                        ExpiryYear = "25",
                        StartMonth = null,
                        StartYear = null,
                        IssueNumber = null
                    },
                    TokenCustomerID = null,
                    Reference = "A12345",
                    Title = "Mr.",
                    FirstName = "John",
                    LastName = "Smith",
                    CompanyName = "Demo Shop 123",
                    JobDescription = "Developer",
                    Street1 = "Level 5",
                    Street2 = "369 Queen Street",
                    City = "Sydney",
                    State = "NSW",
                    PostalCode = "2000",
                    Country = "au",
                    Email = "demo@example.org",
                    Phone = "09 889 0986",
                    Mobile = "09 889 6542",
                    Comments = "",
                    Fax = "",
                    Url = "http://www.ewaypayments.com"
                },
                Payment = new Payment()
                {
                    TotalAmount = 1000,
                    InvoiceNumber = "Inv 21540",
                    InvoiceDescription = "Individual Invoice Description",
                    InvoiceReference = "513456",
                    CurrencyCode = "AUD"
                },
                Errors = null
            };
        }

        //Assertion helpers
        internal static void AssertReturnedCustomerData_VerifyAddressAreEqual(Customer responseCustomer, Customer requestCustomer)
        {
            if (responseCustomer.Address == null)
            {
                Assert.Inconclusive("Response Customer Address not found.");
            }
            Assert.AreEqual(responseCustomer.Address.State, requestCustomer.Address.State);
            Assert.AreEqual(responseCustomer.Address.City, requestCustomer.Address.City);
            Assert.AreEqual(responseCustomer.Address.Country, requestCustomer.Address.Country);
            Assert.AreEqual(responseCustomer.Address.PostalCode, requestCustomer.Address.PostalCode);
            Assert.AreEqual(responseCustomer.Address.Street1, requestCustomer.Address.Street1);
            Assert.AreEqual(responseCustomer.Address.Street2, requestCustomer.Address.Street2);
        }

        internal static void AssertReturnedCustomerData_VerifyCardDetailsAreEqual(Customer responseCustomer, Customer requestCustomer)
        {
            if (!string.IsNullOrWhiteSpace(responseCustomer.CardDetails.ExpiryMonth) &&
                !string.IsNullOrWhiteSpace(requestCustomer.CardDetails.ExpiryMonth))
                Assert.AreEqual(responseCustomer.CardDetails.ExpiryMonth, requestCustomer.CardDetails.ExpiryMonth);

            if (!string.IsNullOrWhiteSpace(responseCustomer.CardDetails.ExpiryYear) &&
                !string.IsNullOrWhiteSpace(requestCustomer.CardDetails.ExpiryYear))
            Assert.AreEqual(responseCustomer.CardDetails.ExpiryYear, requestCustomer.CardDetails.ExpiryYear);

            if (!string.IsNullOrWhiteSpace(responseCustomer.CardDetails.IssueNumber) &&
                !string.IsNullOrWhiteSpace(requestCustomer.CardDetails.IssueNumber))
            Assert.AreEqual(responseCustomer.CardDetails.IssueNumber, requestCustomer.CardDetails.IssueNumber);

            if (!string.IsNullOrWhiteSpace(responseCustomer.CardDetails.Name) &&
                !string.IsNullOrWhiteSpace(requestCustomer.CardDetails.Name))
            Assert.AreEqual(responseCustomer.CardDetails.Name, requestCustomer.CardDetails.Name);

            if (!string.IsNullOrWhiteSpace(responseCustomer.CardDetails.StartMonth) &&
                !string.IsNullOrWhiteSpace(requestCustomer.CardDetails.StartMonth))
            Assert.AreEqual(responseCustomer.CardDetails.StartMonth, requestCustomer.CardDetails.StartMonth);

            if (!string.IsNullOrWhiteSpace(responseCustomer.CardDetails.StartYear) &&
                !string.IsNullOrWhiteSpace(requestCustomer.CardDetails.StartYear))
            Assert.AreEqual(responseCustomer.CardDetails.StartYear, requestCustomer.CardDetails.StartYear);
        }

        internal static void AssertReturnedCustomerData_VerifyAllFieldsAreEqual(Customer responseCustomer, Customer requestCustomer)
        {
            Assert.AreEqual(responseCustomer.Comments, requestCustomer.Comments);
            Assert.AreEqual(responseCustomer.CompanyName, requestCustomer.CompanyName);
            Assert.AreEqual(responseCustomer.Fax, requestCustomer.Fax);
            Assert.AreEqual(responseCustomer.FirstName, requestCustomer.FirstName);
            Assert.AreEqual(responseCustomer.LastName, requestCustomer.LastName);
            Assert.AreEqual(responseCustomer.JobDescription, requestCustomer.JobDescription);
            Assert.AreEqual(responseCustomer.Mobile, requestCustomer.Mobile);
            Assert.AreEqual(responseCustomer.Phone, requestCustomer.Phone);
            Assert.AreEqual(responseCustomer.Reference, requestCustomer.Reference);
            Assert.AreEqual(responseCustomer.Title, requestCustomer.Title);
            Assert.AreEqual(responseCustomer.Url, requestCustomer.Url);
        }
    }
}

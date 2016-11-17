using System;
using System.Linq;
using AutoMapper;
using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
using BaseResponse = eWAY.Rapid.Internals.Response.BaseResponse;
using CardDetails = eWAY.Rapid.Models.CardDetails;
using Customer = eWAY.Rapid.Models.Customer;
using LineItem = eWAY.Rapid.Models.LineItem;
using Option = eWAY.Rapid.Internals.Models.Option;
using Payment = eWAY.Rapid.Internals.Models.Payment;
using Refund = eWAY.Rapid.Models.Refund;
using ShippingAddress = eWAY.Rapid.Models.ShippingAddress;
using VerificationResult = eWAY.Rapid.Models.VerificationResult;


namespace eWAY.Rapid.Internals.Services
{
    internal class MappingProfile : Profile
    {

        public MappingProfile()
        {

            // Requests

            CreateMap<Transaction, DirectPaymentRequest>()
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
               .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
               .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
               .ForMember(dest => dest.CreditCardNumber, opt => opt.Ignore())
               .ForMember(dest => dest.CreditCardCVN, opt => opt.Ignore())
               .ForMember(dest => dest.Method,
                   opt => opt.MapFrom(src => src.Capture ? Method.ProcessPayment : Method.Authorise));

            CreateMap<Transaction, CreateAccessCodeRequest>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.PaymentType, opt => opt.Ignore())
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src =>
                    src.Capture ?
                    (src.Customer.TokenCustomerID == null && src.SaveCustomer != true ? Method.ProcessPayment : Method.TokenPayment)
                    : Method.Authorise
                ));

            CreateMap<Transaction, CapturePaymentRequest>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.AuthTransactionID)).ReverseMap();

            CreateMap<Transaction, CreateAccessCodeSharedRequest>()
                .ForMember(dest => dest.CustomView, opt => opt.MapFrom(src => (CustomView)Enum.Parse(typeof(CustomView), src.CustomView, true)))
                .IncludeBase<Transaction, CreateAccessCodeRequest>()
                .ForMember(dest => dest.AllowedCards, opt => opt.Ignore());

            CreateMap<Customer, DirectPaymentRequest>()
                .ForMember(dest => dest.Method, opt => opt.UseValue(Method.CreateTokenCustomer))
                .ForMember(dest => dest.TransactionType, opt => opt.UseValue(TransactionTypes.MOTO))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.CreditCardNumber, opt => opt.Ignore())
                .ForMember(dest => dest.CreditCardCVN, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore())
                .ForMember(dest => dest.Payment, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceID, opt => opt.Ignore())
                .ForMember(dest => dest.PartnerID, opt => opt.Ignore())
                .ForMember(dest => dest.ThirdPartyWalletID, opt => opt.Ignore());

            CreateMap<Customer, CreateAccessCodeRequest>()
                .ForMember(dest => dest.Method, opt => opt.UseValue(Method.CreateTokenCustomer))
                .ForMember(dest => dest.TransactionType, opt => opt.UseValue(TransactionTypes.MOTO))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore())
                .ForMember(dest => dest.Payment, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceID, opt => opt.Ignore())
                .ForMember(dest => dest.PartnerID, opt => opt.Ignore())
                .ForMember(dest => dest.CancelUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutUrl, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentType, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutPayment, opt => opt.Ignore());

            CreateMap<Customer, CreateAccessCodeSharedRequest>()
                .IncludeBase<Customer, CreateAccessCodeRequest>()
             .ForMember(dest => dest.AllowedCards, opt => opt.Ignore());

            CreateMap<Refund, DirectRefundRequest>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Refund, opt => opt.MapFrom(src => src.RefundDetails));

            CreateMap<CapturePaymentRequest, DirectCapturePaymentRequest>().ReverseMap();
            CreateMap<CancelAuthorisationRequest, DirectCancelAuthorisationRequest>().ReverseMap();

            CreateMap<Transaction, DirectAuthorisationRequest>()
                .IncludeBase<Transaction, DirectPaymentRequest>();

            // Response

            //Errors
            CreateMap<BaseResponse, Rapid.Models.BaseResponse>()
               .AfterMap((src, dest) => dest.Errors = !string.IsNullOrWhiteSpace(src.Errors) ? src.Errors.Split(',').ToList() : null);

            CreateMap<DirectPaymentResponse, CreateTransactionResponse>()
            .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FormActionUrl, opt => opt.Ignore())
                .ForMember(dest => dest.AccessCode, opt => opt.Ignore())
                .ForMember(dest => dest.AmexECEncryptedData, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src)).ReverseMap();

            CreateMap<DirectPaymentResponse, Transaction>()
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src.Payment))
                .ForMember(dest => dest.Capture, opt => opt.Ignore())
                .ForMember(dest => dest.SaveCustomer, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingDetails, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceID, opt => opt.Ignore())
                .ForMember(dest => dest.PartnerID, opt => opt.Ignore())
                .ForMember(dest => dest.ThirdPartyWalletID, opt => opt.Ignore())
                .ForMember(dest => dest.SecuredCardData, opt => opt.Ignore())
                .ForMember(dest => dest.AuthTransactionID, opt => opt.Ignore())
                .ForMember(dest => dest.RedirectURL, opt => opt.Ignore())
                .ForMember(dest => dest.CancelURL, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutURL, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutPayment, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerIP, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerReadOnly, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore())
                .ForMember(dest => dest.CustomView, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerEmail, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerPhone, opt => opt.Ignore())
                .ForMember(dest => dest.HeaderText, opt => opt.Ignore())
                .ForMember(dest => dest.LogoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.FraudAction, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionCaptured, opt => opt.Ignore())
                .ForMember(dest => dest.CurrencyCode, opt => opt.Ignore())
                .ForMember(dest => dest.Source, opt => opt.Ignore())
                .ForMember(dest => dest.MaxRefund, opt => opt.Ignore())
                .ForMember(dest => dest.OriginalTransactionId, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer)).ReverseMap()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<DirectPaymentResponse, TransactionStatus>()
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.BeagleScore,
                    opt => opt.MapFrom(src => src.BeagleScore.HasValue ? src.BeagleScore : 0))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Payment.TotalAmount))
                .ForMember(dest => dest.Captured, opt => opt.Ignore())
                .ForMember(dest => dest.FraudAction, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationResult, opt => opt.MapFrom(src => src.Verification)).ReverseMap();

            CreateMap<DirectPaymentResponse, ProcessingDetails>()
                .ForMember(dest => dest.AuthorisationCode, opt => opt.MapFrom(src => src.AuthorisationCode))
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage)).ReverseMap();


            CreateMap<DirectPaymentResponse, CreateCustomerResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FormActionUrl, opt => opt.Ignore())
                .ForMember(dest => dest.AccessCode, opt => opt.Ignore())
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<CreateAccessCodeResponse, CreateTransactionResponse>()
                .BeforeMap((s, d) => d.Transaction = new Transaction())
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TransactionStatus, opt => opt.Ignore())
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.Ignore())
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<CreateAccessCodeResponse, CreateCustomerResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.Ignore())
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<CreateAccessCodeSharedResponse, CreateCustomerResponse>()
                .IncludeBase<CreateAccessCodeResponse, CreateCustomerResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.MapFrom(src => src.SharedPaymentUrl));

            CreateMap<CreateAccessCodeSharedResponse, CreateTransactionResponse>()
                .IncludeBase<CreateAccessCodeResponse, CreateTransactionResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.MapFrom(src => src.SharedPaymentUrl));

            CreateMap<TransactionSearchResponse, QueryTransactionResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => !src.Transactions.Equals(null) ? src.Transactions.FirstOrDefault() : null))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => !src.Transactions.Equals(null) ? src.Transactions.FirstOrDefault() : null))
                .ForMember(dest => dest.AccessCode, opt => opt.Ignore());

            CreateMap<TransactionResult, QueryTransactionResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.AccessCode, opt => opt.Ignore())
                .ForMember(dest => dest.Errors, opt => opt.Ignore());

            CreateMap<TransactionResult, Transaction>()
                .ForMember(dest => dest.ShippingDetails, opt => opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.Capture, opt => opt.Ignore())
                .ForMember(dest => dest.SaveCustomer, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceID, opt => opt.Ignore())
                .ForMember(dest => dest.PartnerID, opt => opt.Ignore())
                .ForMember(dest => dest.ThirdPartyWalletID, opt => opt.Ignore())
                .ForMember(dest => dest.SecuredCardData, opt => opt.Ignore())
                .ForMember(dest => dest.AuthTransactionID, opt => opt.Ignore())
                .ForMember(dest => dest.RedirectURL, opt => opt.Ignore())
                .ForMember(dest => dest.CancelURL, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutPayment, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutURL, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerIP, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerReadOnly, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore())
                .ForMember(dest => dest.CustomView, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerEmail, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerPhone, opt => opt.Ignore())
                .ForMember(dest => dest.HeaderText, opt => opt.Ignore())
                .ForMember(dest => dest.LogoUrl, opt => opt.Ignore());

            CreateMap<TransactionResult, Customer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.Customer.Reference))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Customer.Title))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
                .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.Customer.JobDescription))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Customer.Phone))
                .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Customer.Mobile))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Customer.Comments))
                .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Customer.Fax))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Customer.Url))
                .ForMember(dest => dest.TokenCustomerID, opt => opt.MapFrom(src => src.TokenCustomerID))
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.RedirectURL, opt => opt.Ignore())
                .ForMember(dest => dest.CardDetails, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerIP, opt => opt.Ignore())
                .ForMember(dest => dest.SecuredCardData, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerReadOnly, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerPhone, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerEmail, opt => opt.Ignore())
                .ForMember(dest => dest.HeaderText, opt => opt.Ignore())
                .ForMember(dest => dest.LogoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore())
                .ForMember(dest => dest.CustomView, opt => opt.Ignore());

            CreateMap<TransactionResult, PaymentDetails>()
                .ForMember(dest => dest.InvoiceReference, opt => opt.MapFrom(src => src.InvoiceReference))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.InvoiceDescription, opt => opt.Ignore());

            CreateMap<TransactionResult, TransactionStatus>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.VerificationResult, opt => opt.MapFrom(src => src.Verification))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<TransactionResult, TransactionStatus>()
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Captured, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationResult, opt => opt.Ignore());

            CreateMap<TransactionResult, ProcessingDetails>()
                .ForMember(dest => dest.AuthorisationCode, opt => opt.MapFrom(src => src.AuthorisationCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage))
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode));

            CreateMap<DirectCustomerSearchResponse, QueryCustomerResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<DirectRefundResponse, RefundResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<DirectCapturePaymentResponse, CreateTransactionResponse>()
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Transaction, opt => opt.Ignore())
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FormActionUrl, opt => opt.Ignore())
                .ForMember(dest => dest.AccessCode, opt => opt.Ignore())
                .ForMember(dest => dest.AmexECEncryptedData, opt => opt.Ignore());

            CreateMap<DirectCapturePaymentResponse, TransactionStatus>()
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.Total, opt => opt.Ignore())
                .ForMember(dest => dest.Captured, opt => opt.Ignore())
                .ForMember(dest => dest.BeagleScore, opt => opt.Ignore())
                .ForMember(dest => dest.FraudAction, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationResult, opt => opt.Ignore())
                .ForMember(dest => dest.ProcessingDetails, opt => opt.Ignore());

            CreateMap<DirectCapturePaymentResponse, ProcessingDetails>()
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage))
                .ForMember(dest => dest.AuthorisationCode, opt => opt.Ignore());

            CreateMap<DirectAuthorisationResponse, CreateTransactionResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.Ignore())
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FormActionUrl, opt => opt.Ignore())
                .ForMember(dest => dest.AccessCode, opt => opt.Ignore())
                .ForMember(dest => dest.AmexECEncryptedData, opt => opt.Ignore());

            CreateMap<DirectCapturePaymentResponse, CapturePaymentResponse>().ReverseMap();
            CreateMap<DirectCancelAuthorisationResponse, CancelAuthorisationResponse>().ReverseMap();

            CreateMap<DirectSettlementSearchResponse, SettlementSearchResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<DirectSettlementSearchResponse, SettlementSearchResponse>().ReverseMap();

            // Custom

            CreateMap<String, Option>().ConvertUsing(s => new Option { Value = s });
            CreateMap<Option, String>().ConvertUsing(o => o.Value);
            //CreateMap<bool?, TransactionStatus>().AfterMap((b, t) => t.Status = b);

            // Entity

            CreateMap<ShippingDetails, Models.ShippingAddress>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ShippingAddress.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.ShippingAddress.Country))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.ShippingAddress.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ShippingAddress.PostalCode))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.ShippingAddress.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.ShippingAddress.Street2))
                .ForMember(dest => dest.ShippingMethod, opt => opt.MapFrom(src => src.ShippingMethod.ToString()))
                .ReverseMap();

            CreateMap<Address, Models.ShippingAddress>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2))
                .ForMember(dest => dest.ShippingMethod, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Phone, opt => opt.Ignore())
                .ForMember(dest => dest.Fax, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RefundDetails, Models.Refund>()
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.OriginalTransactionID.ToString()));

            CreateMap<Models.Refund, RefundDetails>()
                .ForMember(dest => dest.OriginalTransactionID, opt => opt.MapFrom(src => int.Parse(src.TransactionID)));

            long? nullableTokenId = null;

            CreateMap<Customer, Models.Customer>()
                .ForMember(dest => dest.TokenCustomerID, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.TokenCustomerID) ? nullableTokenId : long.Parse(src.TokenCustomerID)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Address.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Address.Street2)).ReverseMap();

            CreateMap<Models.Customer, Customer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.RedirectURL, opt => opt.Ignore())
                .ForMember(dest => dest.CardDetails, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerIP, opt => opt.Ignore())
                .ForMember(dest => dest.SecuredCardData, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerReadOnly, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerEmail, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerPhone, opt => opt.Ignore())
                .ForMember(dest => dest.HeaderText, opt => opt.Ignore())
                .ForMember(dest => dest.LogoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore())
                .ForMember(dest => dest.CustomView, opt => opt.Ignore());

            CreateMap<Models.ShippingAddress, ShippingDetails>()
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src));

            CreateMap<Models.Customer, Address>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2));

            CreateMap<CreateAccessCodeResponse, Transaction>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src.Payment))
                .ForMember(dest => dest.TransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.Capture, opt => opt.Ignore())
                .ForMember(dest => dest.SaveCustomer, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingDetails, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceID, opt => opt.Ignore())
                .ForMember(dest => dest.PartnerID, opt => opt.Ignore())
                .ForMember(dest => dest.ThirdPartyWalletID, opt => opt.Ignore())
                .ForMember(dest => dest.SecuredCardData, opt => opt.Ignore())
                .ForMember(dest => dest.AuthTransactionID, opt => opt.Ignore())
                .ForMember(dest => dest.RedirectURL, opt => opt.Ignore())
                .ForMember(dest => dest.CancelURL, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutURL, opt => opt.Ignore())
                .ForMember(dest => dest.CheckoutPayment, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerIP, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerReadOnly, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore())
                .ForMember(dest => dest.CustomView, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerEmail, opt => opt.Ignore())
                .ForMember(dest => dest.VerifyCustomerPhone, opt => opt.Ignore())
                .ForMember(dest => dest.HeaderText, opt => opt.Ignore())
                .ForMember(dest => dest.LogoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.FraudAction, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionCaptured, opt => opt.Ignore())
                .ForMember(dest => dest.CurrencyCode, opt => opt.Ignore())
                .ForMember(dest => dest.Source, opt => opt.Ignore())
                .ForMember(dest => dest.MaxRefund, opt => opt.Ignore())
                .ForMember(dest => dest.OriginalTransactionId, opt => opt.Ignore());

            CreateMap<DirectTokenCustomer, Customer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<DirectTokenCustomer, Address>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2)).ReverseMap();

            CreateMap<TokenCustomer, Customer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<TokenCustomer, Address>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2)).ReverseMap();

            CreateMap<Customer, TokenCustomer>()
                .ForMember(dest => dest.CardExpiryMonth, opt => opt.MapFrom(src => src.CardDetails.ExpiryMonth))
                .ForMember(dest => dest.CardExpiryYear, opt => opt.MapFrom(src => src.CardDetails.ExpiryYear))
                .ForMember(dest => dest.CardIssueNumber, opt => opt.MapFrom(src => src.CardDetails.IssueNumber))
                .ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardDetails.Name))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardDetails.Number))
                .ForMember(dest => dest.CardStartMonth, opt => opt.MapFrom(src => src.CardDetails.StartMonth))
                .ForMember(dest => dest.CardStartYear, opt => opt.MapFrom(src => src.CardDetails.StartYear))
                .ForMember(dest => dest.IsActiveSpecified, opt => opt.Ignore())
                .IncludeBase<Customer, Models.Customer>()
                .ReverseMap();

            CreateMap<Customer, DirectTokenCustomer>()
                .IncludeBase<Customer, TokenCustomer>()
                .ForMember(dest => dest.IsActiveSpecified, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ShippingAddress, Models.ShippingAddress>().ReverseMap();
            CreateMap<LineItem, Models.LineItem>().ReverseMap();
            CreateMap<Rapid.Models.Option, Option>().ReverseMap();
            CreateMap<PaymentDetails, Payment>().ReverseMap();
            CreateMap<CardDetails, Models.CardDetails>().ReverseMap();
            CreateMap<VerificationResult, Verification>().ReverseMap();
            CreateMap<VerificationResult, Models.VerificationResult>().ReverseMap();
            CreateMap<Rapid.Models.Payment, Payment>().ReverseMap();
            CreateMap<Rapid.Models.SettlementSummary, Models.SettlementSummary>().ReverseMap();
            CreateMap<Rapid.Models.SettlementTransaction, Models.SettlementTransaction>().ReverseMap();
            CreateMap<Rapid.Models.BalanceSummaryPerCardType, Models.BalanceSummaryPerCardType>().ReverseMap();

        }
    }
}
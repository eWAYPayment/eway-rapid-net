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
    internal class MappingService: IMappingService
    {
        public MappingService()
        {
            RegisterMapping();
        }

        public TDest Map<TSource, TDest>(TSource obj)
        {
            return Mapper.Map<TSource, TDest>(obj);
        }

        public static void RegisterMapping()
        {
            RegisterRequestMapping();
            RegisterResponseMapping();
            RegisterCustomMapping();
            RegisterEntitiesMapping();
        }

        public static void RegisterRequestMapping()
        {
            Mapper.CreateMap<Transaction, DirectPaymentRequest>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.Method,
                    opt => opt.MapFrom(src => src.Capture ? Method.ProcessPayment : Method.Authorise));

            Mapper.CreateMap<Transaction, CreateAccessCodeRequest>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Capture
                    ? (src.Customer.TokenCustomerID == null && src.SaveCustomer != true ? Method.ProcessPayment : Method.TokenPayment)
                    : Method.Authorise));

            Mapper.CreateMap<Transaction, CapturePaymentRequest>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.AuthTransactionID)).ReverseMap();

            Mapper.CreateMap<Transaction, CreateAccessCodeSharedRequest>()
                .IncludeBase<Transaction, CreateAccessCodeRequest>();

            Mapper.CreateMap<Customer, DirectPaymentRequest>()
                .ForMember(dest => dest.Method, opt => opt.UseValue(Method.CreateTokenCustomer))
                .ForMember(dest => dest.TransactionType, opt => opt.UseValue(TransactionTypes.Purchase))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src));

            Mapper.CreateMap<Customer, CreateAccessCodeRequest>()
                .ForMember(dest => dest.Method, opt => opt.UseValue(Method.CreateTokenCustomer))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src));

            Mapper.CreateMap<Customer, CreateAccessCodeSharedRequest>()
                .IncludeBase<Customer, CreateAccessCodeRequest>();

            Mapper.CreateMap<Refund, DirectRefundRequest>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Refund, opt => opt.MapFrom(src => src.RefundDetails));

            Mapper.CreateMap<CapturePaymentRequest, DirectCapturePaymentRequest>().ReverseMap();
            Mapper.CreateMap<CancelAuthorisationRequest, DirectCancelAuthorisationRequest>().ReverseMap();

            Mapper.CreateMap<Transaction, DirectAuthorisationRequest>()
                .IncludeBase<Transaction, DirectPaymentRequest>();
        }

        public static void RegisterResponseMapping()
        {
            //Errors
            Mapper.CreateMap<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Errors,
                   opt => opt.ResolveUsing(s => !string.IsNullOrWhiteSpace(s.Errors) ? s.Errors.Split(',').ToList() : null));

            Mapper.CreateMap<DirectPaymentResponse, CreateTransactionResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src)).ReverseMap();

            Mapper.CreateMap<DirectPaymentResponse, Transaction>()
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src.Payment))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer)).ReverseMap()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<DirectPaymentResponse, TransactionStatus>()
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.BeagleScore,
                    opt => opt.MapFrom(src => src.BeagleScore.HasValue ? src.BeagleScore : 0))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Payment.TotalAmount))
                .ForMember(dest => dest.VerificationResult, opt => opt.MapFrom(src => src.Verification)).ReverseMap();

            Mapper.CreateMap<DirectPaymentResponse, ProcessingDetails>()
                .ForMember(dest => dest.AuthorisationCode, opt => opt.MapFrom(src => src.AuthorisationCode))
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage)).ReverseMap();


            Mapper.CreateMap<DirectPaymentResponse, CreateCustomerResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<CreateAccessCodeResponse, CreateTransactionResponse>()
                .BeforeMap((s, d) => d.Transaction = new Transaction())
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<CreateAccessCodeResponse, CreateCustomerResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<CreateAccessCodeSharedResponse, CreateCustomerResponse>()
                .IncludeBase<CreateAccessCodeResponse, CreateCustomerResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.MapFrom(src => src.SharedPaymentUrl));

            Mapper.CreateMap<CreateAccessCodeSharedResponse, CreateTransactionResponse>()
                .IncludeBase<CreateAccessCodeResponse, CreateTransactionResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.MapFrom(src => src.SharedPaymentUrl));
           
            Mapper.CreateMap<TransactionSearchResponse, QueryTransactionResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Transactions.FirstOrDefault()))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src.Transactions.FirstOrDefault()))
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<TransactionResult, QueryTransactionResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src));

            Mapper.CreateMap<TransactionResult, Transaction>()
                .ForMember(dest => dest.ShippingDetails, opt => opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src));
                
            Mapper.CreateMap<TransactionResult, Customer>()
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
                .ForMember(dest => dest.TokenCustomerID, opt => opt.MapFrom(src => src.TokenCustomerID));

            Mapper.CreateMap<TransactionResult, PaymentDetails>()
                .ForMember(dest => dest.InvoiceReference, opt => opt.MapFrom(src => src.InvoiceReference))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            Mapper.CreateMap<TransactionResult, TransactionStatus>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.VerificationResult, opt => opt.MapFrom(src => src.Verification))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalAmount));

            Mapper.CreateMap<TransactionResult, TransactionStatus>()
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src));

            Mapper.CreateMap<TransactionResult, ProcessingDetails>()
                .ForMember(dest => dest.AuthorisationCode, opt => opt.MapFrom(src => src.AuthorisationCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage))
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode));

            Mapper.CreateMap<DirectCustomerSearchResponse, QueryCustomerResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<DirectRefundResponse, RefundResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<DirectCapturePaymentResponse, CreateTransactionResponse>()
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src));
            Mapper.CreateMap<DirectCapturePaymentResponse, TransactionStatus>()
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus));
            Mapper.CreateMap<DirectCapturePaymentResponse, ProcessingDetails>()
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage));

            Mapper.CreateMap<DirectAuthorisationResponse, CreateTransactionResponse>()
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            Mapper.CreateMap<DirectCapturePaymentResponse, CapturePaymentResponse>().ReverseMap();
            Mapper.CreateMap<DirectCancelAuthorisationResponse, CancelAuthorisationResponse>().ReverseMap();
        }

        public static void RegisterCustomMapping()
        {
            Mapper.CreateMap<String, Option>().ConvertUsing(s => new Option { Value = s });
            Mapper.CreateMap<Option, String>().ConvertUsing(o => o.Value);
            Mapper.CreateMap<bool?, TransactionStatus>().AfterMap((b, t) => t.Status = b);
        }

        public static void RegisterEntitiesMapping()
        {
            Mapper.CreateMap<ShippingDetails, Models.ShippingAddress>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ShippingAddress.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.ShippingAddress.Country))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.ShippingAddress.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ShippingAddress.PostalCode))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.ShippingAddress.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.ShippingAddress.Street2))
                .ForMember(dest => dest.ShippingMethod, opt => opt.MapFrom(src => src.ShippingMethod.ToString()))
                .ReverseMap();

            Mapper.CreateMap<Address, Models.ShippingAddress>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2))
                .ReverseMap();

            Mapper.CreateMap<RefundDetails, Models.Refund>()
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.OriginalTransactionID.ToString()));

            Mapper.CreateMap<Models.Refund, RefundDetails>()
                .ForMember(dest => dest.OriginalTransactionID, opt => opt.MapFrom(src => int.Parse(src.TransactionID)));

            long? nullableTokenId = null;

            Mapper.CreateMap<Customer, Models.Customer>()
                .ForMember(dest => dest.TokenCustomerID, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.TokenCustomerID) ? nullableTokenId : long.Parse(src.TokenCustomerID)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Address.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Address.Street2)).ReverseMap();

            Mapper.CreateMap<Models.Customer, Customer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));

            Mapper.CreateMap<Models.ShippingAddress, ShippingDetails>()
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src));

            Mapper.CreateMap<Models.Customer, Address>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2));

            Mapper.CreateMap<CreateAccessCodeResponse, Transaction>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src.Payment));

            Mapper.CreateMap<DirectTokenCustomer, Customer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ReverseMap();

            Mapper.CreateMap<DirectTokenCustomer, Address>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2)).ReverseMap();

            Mapper.CreateMap<TokenCustomer, Customer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ReverseMap();

            Mapper.CreateMap<TokenCustomer, Address>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Street1, opt => opt.MapFrom(src => src.Street1))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Street2)).ReverseMap();

            Mapper.CreateMap<Customer, TokenCustomer>()
                .ForMember(dest => dest.CardExpiryMonth, opt => opt.MapFrom(src => src.CardDetails.ExpiryMonth))
                .ForMember(dest => dest.CardExpiryYear, opt => opt.MapFrom(src => src.CardDetails.ExpiryYear))
                .ForMember(dest => dest.CardIssueNumber, opt => opt.MapFrom(src => src.CardDetails.IssueNumber))
                .ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardDetails.Name))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardDetails.Number))
                .ForMember(dest => dest.CardStartMonth, opt => opt.MapFrom(src => src.CardDetails.StartMonth))
                .ForMember(dest => dest.CardStartYear, opt => opt.MapFrom(src => src.CardDetails.StartYear))
                .IncludeBase<Customer, Models.Customer>()
                .ReverseMap();

            Mapper.CreateMap<Customer, DirectTokenCustomer>()
                .IncludeBase<Customer, TokenCustomer>()
                .ReverseMap();

            Mapper.CreateMap<ShippingAddress, Models.ShippingAddress>().ReverseMap();
            Mapper.CreateMap<LineItem, Models.LineItem>().ReverseMap();
            Mapper.CreateMap<Rapid.Models.Option, Option>().ReverseMap();
            Mapper.CreateMap<PaymentDetails, Payment>().ReverseMap();
            Mapper.CreateMap<CardDetails, Models.CardDetails>().ReverseMap();
            Mapper.CreateMap<VerificationResult, Verification>().ReverseMap();
            Mapper.CreateMap<VerificationResult, Models.VerificationResult>().ReverseMap();
            Mapper.CreateMap<Rapid.Models.Payment, Payment>().ReverseMap();
        }
    }
}

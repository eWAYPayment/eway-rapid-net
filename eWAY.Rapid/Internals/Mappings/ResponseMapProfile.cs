using System.Linq;
using AutoMapper;
using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
using BaseResponse = eWAY.Rapid.Internals.Response.BaseResponse;
using Customer = eWAY.Rapid.Models.Customer;

namespace eWAY.Rapid.Internals.Mappings {
    internal class ResponseMapProfile : Profile {
        public ResponseMapProfile() {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            //Errors
            CreateMap<BaseResponse, Rapid.Models.BaseResponse>(MemberList.Destination)
                .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Errors) ? null : src.Errors.Split(',').ToList()));

            CreateMap<DirectPaymentResponse, CreateTransactionResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<DirectPaymentResponse, Transaction>(MemberList.Destination)
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src.Payment))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ReverseMap();

            CreateMap<DirectPaymentResponse, TransactionStatus>(MemberList.Destination)
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.BeagleScore, opt => opt.MapFrom(src => src.BeagleScore.HasValue ? src.BeagleScore : 0))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Payment.TotalAmount))
                .ForMember(dest => dest.VerificationResult, opt => opt.MapFrom(src => src.Verification))
                .ReverseMap();

            CreateMap<DirectPaymentResponse, ProcessingDetails>(MemberList.Destination)
                .ForMember(dest => dest.AuthorisationCode, opt => opt.MapFrom(src => src.AuthorisationCode))
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage))
                .ReverseMap();

            CreateMap<DirectPaymentResponse, CreateCustomerResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<CreateAccessCodeResponse, CreateTransactionResponse>(MemberList.Destination)
                .BeforeMap((s, d) => d.Transaction = new Transaction())
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src));

            CreateMap<CreateAccessCodeResponse, CreateCustomerResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<CreateAccessCodeSharedResponse, CreateCustomerResponse>(MemberList.Destination)
                .IncludeBase<CreateAccessCodeResponse, CreateCustomerResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.MapFrom(src => src.SharedPaymentUrl));

            CreateMap<CreateAccessCodeSharedResponse, CreateTransactionResponse>(MemberList.Destination)
                .IncludeBase<CreateAccessCodeResponse, CreateTransactionResponse>()
                .ForMember(dest => dest.SharedPaymentUrl, opt => opt.MapFrom(src => src.SharedPaymentUrl));

            CreateMap<TransactionSearchResponse, QueryTransactionResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => !src.Transactions.Equals(null) ? src.Transactions.FirstOrDefault() : null))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => !src.Transactions.Equals(null) ? src.Transactions.FirstOrDefault() : null));

            CreateMap<TransactionResult, QueryTransactionResponse>(MemberList.Destination)
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src));

            CreateMap<TransactionResult, Transaction>(MemberList.Destination)
                .ForMember(dest => dest.ShippingDetails, opt => opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src));

            CreateMap<TransactionResult, Customer>(MemberList.Destination)
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

            CreateMap<TransactionResult, PaymentDetails>(MemberList.Destination)
                .ForMember(dest => dest.InvoiceReference, opt => opt.MapFrom(src => src.InvoiceReference))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<TransactionResult, TransactionStatus>(MemberList.Destination)
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.ProcessingDetails, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.VerificationResult, opt => opt.MapFrom(src => src.Verification))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<TransactionResult, ProcessingDetails>(MemberList.Destination)
                .ForMember(dest => dest.AuthorisationCode, opt => opt.MapFrom(src => src.AuthorisationCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage))
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode));

            CreateMap<DirectCustomerSearchResponse, QueryCustomerResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<DirectRefundResponse, RefundResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<DirectCapturePaymentResponse, CreateTransactionResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src));

            CreateMap<DirectCapturePaymentResponse, TransactionStatus>(MemberList.Destination)
                .ForMember(dest => dest.TransactionID, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TransactionStatus));

            CreateMap<DirectCapturePaymentResponse, ProcessingDetails>(MemberList.Destination)
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode))
                .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage));

            CreateMap<DirectAuthorisationResponse, CreateTransactionResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>();

            CreateMap<DirectCapturePaymentResponse, CapturePaymentResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ReverseMap();

            CreateMap<DirectCancelAuthorisationResponse, CancelAuthorisationResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ReverseMap();

            CreateMap<DirectSettlementSearchResponse, SettlementSearchResponse>(MemberList.Destination)
                .IncludeBase<BaseResponse, Rapid.Models.BaseResponse>()
                .ReverseMap();
        }
    }
}

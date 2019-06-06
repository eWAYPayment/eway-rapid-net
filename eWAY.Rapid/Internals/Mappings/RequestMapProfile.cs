using AutoMapper;
using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Models;
using Customer = eWAY.Rapid.Models.Customer;
using Refund = eWAY.Rapid.Models.Refund;

namespace eWAY.Rapid.Internals.Mappings {
    internal class RequestMapProfile : Profile {
        public RequestMapProfile() {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<Transaction, DirectPaymentRequest>(MemberList.None)
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Capture ? Method.ProcessPayment : Method.Authorise));

            CreateMap<Transaction, CreateAccessCodeRequest>(MemberList.None)
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Capture
                    ? (src.Customer.TokenCustomerID == null && src.SaveCustomer != true ? Method.ProcessPayment : Method.TokenPayment)
                    : Method.Authorise));

            CreateMap<Transaction, CapturePaymentRequest>(MemberList.None)
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.AuthTransactionID)).ReverseMap();

            CreateMap<Transaction, CreateAccessCodeSharedRequest>()
                .IncludeBase<Transaction, CreateAccessCodeRequest>();

            CreateMap<Customer, DirectPaymentRequest>(MemberList.None)
                .ForMember(dest => dest.Method, opt => opt.UseValue(Method.CreateTokenCustomer))
                .ForMember(dest => dest.TransactionType, opt => opt.UseValue(TransactionTypes.MOTO))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src));

            CreateMap<Customer, CreateAccessCodeRequest>(MemberList.None)
                .ForMember(dest => dest.Method, opt => opt.UseValue(Method.CreateTokenCustomer))
                .ForMember(dest => dest.TransactionType, opt => opt.UseValue(TransactionTypes.MOTO))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src));

            CreateMap<Customer, CreateAccessCodeSharedRequest>(MemberList.None)
                .IncludeBase<Customer, CreateAccessCodeRequest>();

            CreateMap<Refund, DirectRefundRequest>(MemberList.None)
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingDetails))
                .ForMember(dest => dest.Refund, opt => opt.MapFrom(src => src.RefundDetails));

            CreateMap<CapturePaymentRequest, DirectCapturePaymentRequest>(MemberList.None)
                .ReverseMap();

            CreateMap<CancelAuthorisationRequest, DirectCancelAuthorisationRequest>(MemberList.None)
                .ReverseMap();

            CreateMap<Transaction, DirectAuthorisationRequest>(MemberList.None)
                .IncludeBase<Transaction, DirectPaymentRequest>();
        }
    }
}

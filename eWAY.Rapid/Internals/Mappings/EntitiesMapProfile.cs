using AutoMapper;
using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
using Customer = eWAY.Rapid.Models.Customer;
using Option = eWAY.Rapid.Internals.Models.Option;

namespace eWAY.Rapid.Internals.Mappings {
    internal class EntitiesMapProfile : Profile {
        public EntitiesMapProfile() {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

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
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));

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
                .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src.Payment));

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
                .IncludeBase<Customer, Models.Customer>()
                .ReverseMap();

            CreateMap<Customer, DirectTokenCustomer>()
                .IncludeBase<Customer, TokenCustomer>()
                .ReverseMap();

            CreateMap<Rapid.Models.ShippingAddress, Models.ShippingAddress>()
                .ReverseMap();

            CreateMap<Rapid.Models.LineItem, Models.LineItem>()
                .ReverseMap();

            CreateMap<Rapid.Models.Option, Models.Option>()
                .ReverseMap();

            CreateMap<Rapid.Models.PaymentDetails, Models.Payment>()
                .ReverseMap();

            CreateMap<Rapid.Models.CardDetails, Models.CardDetails>()
                .ReverseMap();

            CreateMap<Rapid.Models.Verification, Models.VerificationResult>()
                .ReverseMap();

            CreateMap<Rapid.Models.VerificationResult, Rapid.Models.Verification>()
                .ReverseMap();

            CreateMap<Rapid.Models.VerificationResult, Models.VerificationResult>()
                .ReverseMap();

            CreateMap<Rapid.Models.Payment, Models.Payment>()
                .ReverseMap();

            CreateMap<Rapid.Models.SettlementSummary, Models.SettlementSummary>()
                .ReverseMap();

            CreateMap<Rapid.Models.SettlementTransaction, Models.SettlementTransaction>()
                .ReverseMap();

            CreateMap<Rapid.Models.BalanceSummaryPerCardType, Models.BalanceSummaryPerCardType>()
                .ReverseMap();
        }
    }
}

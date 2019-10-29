using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
using Customer = eWAY.Rapid.Models.Customer;
using Option = eWAY.Rapid.Internals.Models.Option;
using Mapster;

namespace eWAY.Rapid.Internals.Mappings {
    internal class EntitiesMapProfile {
        public static void CreateEntitiesMapProfile(IAdapter adapter) {

            adapter.BuildAdapter(TypeAdapterConfig<ShippingDetails, Models.ShippingAddress>.NewConfig()
                .Map(dest => dest.City, src => src.ShippingAddress.City)
               .Map(dest => dest.Country, src => src.ShippingAddress.Country)
               .Map(dest => dest.State, src => src.ShippingAddress.State)
               .Map(dest => dest.PostalCode, src => src.ShippingAddress.PostalCode)
               .Map(dest => dest.Street1, src => src.ShippingAddress.Street1)
               .Map(dest => dest.Street2, src => src.ShippingAddress.Street2)
               .Map(dest => dest.ShippingMethod, src => src.ShippingMethod.ToString()).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Address, Models.ShippingAddress>.NewConfig()
                 
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.State, src => src.State)
                .Map(dest => dest.PostalCode, src => src.PostalCode)
                .Map(dest => dest.Street1, src => src.Street1)
                .Map(dest => dest.Street2, src => src.Street2).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<RefundDetails, Models.Refund>.NewConfig()
                .Map(dest => dest.TransactionID, src => src.OriginalTransactionID.ToString()));

            adapter.BuildAdapter(TypeAdapterConfig<Models.Refund, RefundDetails>.NewConfig()
                .Map(dest => dest.OriginalTransactionID, src => int.Parse(src.TransactionID)));

            long? nullableTokenId = null;

            adapter.BuildAdapter(TypeAdapterConfig<Customer, Models.Customer>.NewConfig()
                
                .Map(dest => dest.TokenCustomerID, src => string.IsNullOrWhiteSpace(src.TokenCustomerID) ? nullableTokenId : long.Parse(src.TokenCustomerID))
                .Map(dest => dest.City, src => src.Address.City)
                .Map(dest => dest.Country, src => src.Address.Country)
                .Map(dest => dest.PostalCode, src => src.Address.PostalCode)
                .Map(dest => dest.State, src => src.Address.State)
                .Map(dest => dest.Street1, src => src.Address.Street1)
                .Map(dest => dest.Street2, src => src.Address.Street2).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Models.Customer, Customer>.NewConfig()
                .Map(dest => dest.Address, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<Models.ShippingAddress, ShippingDetails>.NewConfig()
                .Map(dest => dest.ShippingAddress, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<Models.Customer, Address>.NewConfig()
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.PostalCode, src => src.PostalCode)
                .Map(dest => dest.State, src => src.State)
                .Map(dest => dest.Street1, src => src.Street1)
                .Map(dest => dest.Street2, src => src.Street2));

            adapter.BuildAdapter(TypeAdapterConfig<CreateAccessCodeResponse, Transaction>.NewConfig()
                .Map(dest => dest.Customer, src => src.Customer)
                .Map(dest => dest.PaymentDetails, src => src.Payment));

            adapter.BuildAdapter(TypeAdapterConfig<DirectTokenCustomer, Customer>.NewConfig()
                
                .Map(dest => dest.Address, src => src).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<DirectTokenCustomer, Address>.NewConfig()
                .TwoWays()
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.PostalCode, src => src.PostalCode)
                .Map(dest => dest.State, src => src.State)
                .Map(dest => dest.Street1, src => src.Street1)
                .Map(dest => dest.Street2, src => src.Street2));

            adapter.BuildAdapter(TypeAdapterConfig<TokenCustomer, Customer>.NewConfig()
                
                .Map(dest => dest.Address, src => src).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<TokenCustomer, Address>.NewConfig()
                
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.PostalCode, src => src.PostalCode)
                .Map(dest => dest.State, src => src.State)
                .Map(dest => dest.Street1, src => src.Street1)
                .Map(dest => dest.Street2, src => src.Street2).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Customer, TokenCustomer>.NewConfig()
                
                .Map(dest => dest.CardExpiryMonth, src => src.CardDetails.ExpiryMonth)
                .Map(dest => dest.CardExpiryYear, src => src.CardDetails.ExpiryYear)
                .Map(dest => dest.CardIssueNumber, src => src.CardDetails.IssueNumber)
                .Map(dest => dest.CardName, src => src.CardDetails.Name)
                .Map(dest => dest.CardNumber, src => src.CardDetails.Number)
                .Map(dest => dest.CardStartMonth, src => src.CardDetails.StartMonth)
                .Map(dest => dest.CardStartYear, src => src.CardDetails.StartYear)
                .Inherits<Customer, Models.Customer>().TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Customer, DirectTokenCustomer>.NewConfig()
                
                .Inherits<Customer, TokenCustomer>().TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.ShippingAddress, Models.ShippingAddress>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.LineItem, Models.LineItem>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.Option, Models.Option>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.PaymentDetails, Models.Payment>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.CardDetails, Models.CardDetails>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.Verification, Models.VerificationResult>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.VerificationResult, Rapid.Models.Verification>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.VerificationResult, Models.VerificationResult>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.Payment, Models.Payment>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.SettlementSummary, Models.SettlementSummary>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.SettlementTransaction, Models.SettlementTransaction>.NewConfig()
                .TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Rapid.Models.BalanceSummaryPerCardType, Models.BalanceSummaryPerCardType>.NewConfig()
                .TwoWays());
        }
    }
}

using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Models;
using Mapster;
using Customer = eWAY.Rapid.Models.Customer;
using Refund = eWAY.Rapid.Models.Refund;

namespace eWAY.Rapid.Internals.Mappings {
    internal class RequestMapProfile {
        public static void CreateRequestMapProfile(IAdapter adapter) {

            adapter.BuildAdapter(TypeAdapterConfig<Transaction, DirectPaymentRequest>
                .NewConfig()
                .Map(dest => dest.Items, src => src.LineItems)
                .Map(dest => dest.ShippingAddress, src => src.ShippingDetails)
                .Map(dest => dest.Payment, src => src.PaymentDetails)
                .Map(dest => dest.Method, src => src.Capture ? Method.ProcessPayment : Method.Authorise));


            adapter.BuildAdapter(TypeAdapterConfig<Transaction, DirectPaymentRequest>.NewConfig()
                .Map(dest => dest.Items, src => src.LineItems)
                .Map(dest => dest.ShippingAddress, src => src.ShippingDetails)
                .Map(dest => dest.Payment, src => src.PaymentDetails)
                .Map(dest => dest.Method, src => src.Capture ? Method.ProcessPayment : Method.Authorise));

            adapter.BuildAdapter(TypeAdapterConfig<Transaction, CreateAccessCodeRequest>.NewConfig()
                .Map(dest => dest.Items, src => src.LineItems)
                .Map(dest => dest.ShippingAddress, src => src.ShippingDetails)
                .Map(dest => dest.Payment, src => src.PaymentDetails)
                .Map(dest => dest.Method, src => src.Capture
                    ? (src.Customer.TokenCustomerID == null && src.SaveCustomer != true ? Method.ProcessPayment : Method.TokenPayment)
                    : Method.Authorise));

            adapter.BuildAdapter(TypeAdapterConfig<Transaction, CapturePaymentRequest>.NewConfig().TwoWays()
                .Map(dest => dest.Payment, src => src.PaymentDetails)
                .Map(dest => dest.TransactionId, src => src.AuthTransactionID));

            adapter.BuildAdapter(TypeAdapterConfig<Transaction, CreateAccessCodeSharedRequest>.NewConfig()
                .Inherits<Transaction, CreateAccessCodeRequest>());

            adapter.BuildAdapter(TypeAdapterConfig<Customer, DirectPaymentRequest>.NewConfig()
                .Map(dest => dest.Method, a => Method.CreateTokenCustomer)
                .Map(dest => dest.TransactionType, a => TransactionTypes.Purchase)
                .Map(dest => dest.Customer, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<Customer, CreateAccessCodeRequest>.NewConfig()
                .Map(dest => dest.Method, a => Method.CreateTokenCustomer)
                .Map(dest => dest.Customer, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<Customer, CreateAccessCodeSharedRequest>.NewConfig()
                .Inherits<Customer, CreateAccessCodeRequest>());

            adapter.BuildAdapter(TypeAdapterConfig<Refund, DirectRefundRequest>.NewConfig()
                .Map(dest => dest.Items, src => src.LineItems)
                .Map(dest => dest.ShippingAddress, src => src.ShippingDetails)
                .Map(dest => dest.Refund, src => src.RefundDetails));

            adapter.BuildAdapter(TypeAdapterConfig<CapturePaymentRequest, DirectCapturePaymentRequest>
                .NewConfig().TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<CancelAuthorisationRequest, DirectCancelAuthorisationRequest>
                .NewConfig().TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<Transaction, DirectAuthorisationRequest>.NewConfig()
                .Inherits<Transaction, DirectPaymentRequest>());
        }
    }
}

using System.Linq;
using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
using Mapster;
using BaseResponse = eWAY.Rapid.Internals.Response.BaseResponse;
using Customer = eWAY.Rapid.Models.Customer;

namespace eWAY.Rapid.Internals.Mappings {
    internal class ResponseMapProfile {
        public static void CreateResponseMapProfile(IAdapter adapter) {

            //Errors
            adapter.BuildAdapter(TypeAdapterConfig<BaseResponse, Rapid.Models.BaseResponse>.NewConfig()
                .Map(dest => dest.Errors, src => string.IsNullOrWhiteSpace(src.Errors) ? null : src.Errors.Split(',').ToList()));

            adapter.BuildAdapter(TypeAdapterConfig<DirectPaymentResponse, CreateTransactionResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>()
                .Map(dest => dest.Transaction, src => src)
                .Map(dest => dest.TransactionStatus, src => src).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<DirectPaymentResponse, Transaction>.NewConfig()
                .Map(dest => dest.PaymentDetails, src => src.Payment)
                .Map(dest => dest.Customer, src => src.Customer));

            adapter.BuildAdapter(TypeAdapterConfig<DirectPaymentResponse, TransactionStatus>.NewConfig()
                .Map(dest => dest.ProcessingDetails, src => src)
                .Map(dest => dest.Status, src => src.TransactionStatus)
                .Map(dest => dest.TransactionID, src => src.TransactionID)
                .Map(dest => dest.BeagleScore, src => src.BeagleScore ?? 0)
                .Map(dest => dest.Total, src => src.Payment.TotalAmount)
                .Map(dest => dest.VerificationResult, src => src.Verification).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<DirectPaymentResponse, ProcessingDetails>.NewConfig()
                .Map(dest => dest.AuthorisationCode, src => src.AuthorisationCode)
                .Map(dest => dest.ResponseCode, src => src.ResponseCode)
                .Map(dest => dest.ResponseMessage, src => src.ResponseMessage).TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<DirectPaymentResponse, CreateCustomerResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>());

            adapter.BuildAdapter(TypeAdapterConfig<CreateAccessCodeResponse, CreateTransactionResponse>.NewConfig()
                .BeforeMapping((s, d) => d.Transaction = new Transaction())
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>()
                .Map(dest => dest.Transaction, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<CreateAccessCodeResponse, CreateCustomerResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>());

            adapter.BuildAdapter(TypeAdapterConfig<CreateAccessCodeSharedResponse, CreateCustomerResponse>.NewConfig()
                .Inherits<CreateAccessCodeResponse, CreateCustomerResponse>()
                .Map(dest => dest.SharedPaymentUrl, src => src.SharedPaymentUrl));

            adapter.BuildAdapter(TypeAdapterConfig<CreateAccessCodeSharedResponse, CreateTransactionResponse>.NewConfig()
                .Inherits<CreateAccessCodeResponse, CreateTransactionResponse>()
                .Map(dest => dest.SharedPaymentUrl, src => src.SharedPaymentUrl));

            adapter.BuildAdapter(TypeAdapterConfig<TransactionSearchResponse, QueryTransactionResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>()
                .Map(dest => dest.Transaction, src => !src.Transactions.Equals(null) ? src.Transactions.FirstOrDefault() : null)
                .Map(dest => dest.TransactionStatus, src => !src.Transactions.Equals(null) ? src.Transactions.FirstOrDefault() : null));

            adapter.BuildAdapter(TypeAdapterConfig<TransactionResult, QueryTransactionResponse>.NewConfig()
                .Map(dest => dest.Transaction, src => src)
                .Map(dest => dest.TransactionStatus, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<TransactionResult, Transaction>.NewConfig()
                .Map(dest => dest.ShippingDetails, src => src.ShippingAddress)
                .Map(dest => dest.PaymentDetails, src => src)
                .Map(dest => dest.Customer, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<TransactionResult, Customer>.NewConfig()
                .Map(dest => dest.Address, src => src.Customer)
                .Map(dest => dest.Reference, src => src.Customer.Reference)
                .Map(dest => dest.Title, src => src.Customer.Title)
                .Map(dest => dest.FirstName, src => src.Customer.FirstName)
                .Map(dest => dest.LastName, src => src.Customer.LastName)
                .Map(dest => dest.CompanyName, src => src.Customer.CompanyName)
                .Map(dest => dest.JobDescription, src => src.Customer.JobDescription)
                .Map(dest => dest.Email, src => src.Customer.Email)
                .Map(dest => dest.Phone, src => src.Customer.Phone)
                .Map(dest => dest.Mobile, src => src.Customer.Mobile)
                .Map(dest => dest.Comments, src => src.Customer.Comments)
                .Map(dest => dest.Fax, src => src.Customer.Fax)
                .Map(dest => dest.Url, src => src.Customer.Url)
                .Map(dest => dest.TokenCustomerID, src => src.TokenCustomerID));

            adapter.BuildAdapter(TypeAdapterConfig<TransactionResult, PaymentDetails>.NewConfig()
                .Map(dest => dest.InvoiceReference, src => src.InvoiceReference)
                .Map(dest => dest.InvoiceNumber, src => src.InvoiceNumber)
                .Map(dest => dest.TotalAmount, src => src.TotalAmount));

            adapter.BuildAdapter(TypeAdapterConfig<TransactionResult, TransactionStatus>.NewConfig()
                .Map(dest => dest.Status, src => src.TransactionStatus)
                .Map(dest => dest.TransactionID, src => src.TransactionID)
                .Map(dest => dest.ProcessingDetails, src => src)
                .Map(dest => dest.VerificationResult, src => src.Verification)
                .Map(dest => dest.Total, src => src.TotalAmount));

            adapter.BuildAdapter(TypeAdapterConfig<TransactionResult, ProcessingDetails>.NewConfig()
                .Map(dest => dest.AuthorisationCode, src => src.AuthorisationCode)
                .Map(dest => dest.ResponseMessage, src => src.ResponseMessage)
                .Map(dest => dest.ResponseCode, src => src.ResponseCode));

            adapter.BuildAdapter(TypeAdapterConfig<DirectCustomerSearchResponse, QueryCustomerResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>());

            adapter.BuildAdapter(TypeAdapterConfig<DirectRefundResponse, RefundResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>());

            adapter.BuildAdapter(TypeAdapterConfig<DirectCapturePaymentResponse, CreateTransactionResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>()
                .Map(dest => dest.TransactionStatus, src => src));

            adapter.BuildAdapter(TypeAdapterConfig<DirectCapturePaymentResponse, TransactionStatus>.NewConfig()
                .Map(dest => dest.TransactionID, src => src.TransactionID)
                .Map(dest => dest.Status, src => src.TransactionStatus));

            adapter.BuildAdapter(TypeAdapterConfig<DirectCapturePaymentResponse, ProcessingDetails>.NewConfig()
                .Map(dest => dest.ResponseCode, src => src.ResponseCode)
                .Map(dest => dest.ResponseMessage, src => src.ResponseMessage));

            adapter.BuildAdapter(TypeAdapterConfig<DirectAuthorisationResponse, CreateTransactionResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>());

            adapter.BuildAdapter(TypeAdapterConfig<DirectCapturePaymentResponse, CapturePaymentResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>().TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<DirectCancelAuthorisationResponse, CancelAuthorisationResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>().TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<DirectSettlementSearchResponse, SettlementSearchResponse>.NewConfig()
                .Inherits<BaseResponse, Rapid.Models.BaseResponse>().TwoWays());

            adapter.BuildAdapter(TypeAdapterConfig<DirectTokenCustomer, Customer>.NewConfig()
                .Map(dest => dest.Address, src => src));
        }
    }
}

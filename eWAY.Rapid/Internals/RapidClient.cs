using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Internals.Services;
using eWAY.Rapid.Models;
using BaseResponse = eWAY.Rapid.Models.BaseResponse;

namespace eWAY.Rapid.Internals
{
    internal class RapidClient : IRapidClient
    {
        private readonly IRapidService _rapidService;
        private readonly IMappingService _mappingService;

        public List<string> ErrorCodes
        {
            get
            {
                return _rapidService.GetErrorCodes();
            }
        }

        public RapidClient(IRapidService rapidService)
        {
            _rapidService = rapidService;
            _mappingService = new MappingService();
        }

        public void SetCredentials(string apiKey, string password)
        {
            _rapidService.SetCredentials(apiKey, password);
        }

        public void SetVersion(int version)
        {
            _rapidService.SetVersion(version);
        }

        private CreateTransactionResponse CreateInternal(PaymentMethod paymentMethod, Transaction transaction)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.Direct:
                  return CreateTransaction<DirectPaymentRequest, DirectPaymentResponse>(_rapidService.DirectPayment, transaction);
                case PaymentMethod.TransparentRedirect:
                    return CreateTransaction<CreateAccessCodeRequest, CreateAccessCodeResponse>(_rapidService.CreateAccessCode, transaction);
                case PaymentMethod.ResponsiveShared:
                    return CreateTransaction<CreateAccessCodeSharedRequest, CreateAccessCodeSharedResponse>(_rapidService.CreateAccessCodeShared, transaction);
                case PaymentMethod.Authorisation:
                    return CreateTransaction<DirectAuthorisationRequest, DirectAuthorisationResponse>(_rapidService.DirectAuthorisation, transaction);
                case PaymentMethod.Wallet:
                    return transaction.Capture ? CreateTransaction<DirectPaymentRequest, DirectPaymentResponse>(_rapidService.DirectPayment, transaction) : 
                        CreateTransaction<DirectAuthorisationRequest, DirectAuthorisationResponse>(_rapidService.DirectAuthorisation, transaction);
            }
            throw new NotSupportedException("Invalid PaymentMethod");
        }

        private CreateCustomerResponse CreateInternal(PaymentMethod paymentMethod, Customer customer)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.Direct:
                    return CreateCustomer<DirectPaymentRequest, DirectPaymentResponse>(_rapidService.DirectPayment, customer);
                case PaymentMethod.TransparentRedirect:
                    return CreateCustomer<CreateAccessCodeRequest, CreateAccessCodeResponse>(_rapidService.CreateAccessCode, customer);
                case PaymentMethod.ResponsiveShared:
                    return CreateCustomer<CreateAccessCodeSharedRequest, CreateAccessCodeSharedResponse>(_rapidService.CreateAccessCodeShared, customer);
            }
            throw new NotSupportedException("Invalid PaymentMethod");
        }

        private CreateCustomerResponse UpdateInternal(PaymentMethod paymentMethod, Customer customer)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.Direct:
                    return CreateCustomer<DirectPaymentRequest, DirectPaymentResponse>(_rapidService.UpdateCustomerDirectPayment, customer);
                case PaymentMethod.TransparentRedirect:
                    return CreateCustomer<CreateAccessCodeRequest, CreateAccessCodeResponse>(_rapidService.UpdateCustomerCreateAccessCode, customer);
                case PaymentMethod.ResponsiveShared:
                    return CreateCustomer<CreateAccessCodeSharedRequest, CreateAccessCodeSharedResponse>(_rapidService.UpdateCustomerCreateAccessCodeShared, customer);
            }
            throw new NotSupportedException("Invalid PaymentMethod");
        }


        public CreateTransactionResponse Create(PaymentMethod paymentMethod, Transaction transaction)
        {
            if (!IsValid) return SdkInvalidStateErrorsResponse<CreateTransactionResponse>();
            var response = CreateInternal(paymentMethod, transaction);
            return response;
        }

        public CreateCustomerResponse Create(PaymentMethod paymentMethod, Customer customer)
        {
            if (!IsValid) return SdkInvalidStateErrorsResponse<CreateCustomerResponse>();
            var response = CreateInternal(paymentMethod, customer);
            return response;
        }

        public CreateCustomerResponse UpdateCustomer(PaymentMethod paymentMethod, Customer customer)
        {
            if (!IsValid) return SdkInvalidStateErrorsResponse<CreateCustomerResponse>();
            var response = UpdateInternal(paymentMethod, customer);
            return response;
        }

        TResponse SdkInternalErrorsResponse<TResponse>() where TResponse: BaseResponse, new()
        {
            return new TResponse() 
            { 
                Errors = new List<string>(new[] { RapidSystemErrorCode.INTERNAL_SDK_ERROR }) 
            };
        }
        TResponse SdkInvalidStateErrorsResponse<TResponse>() where TResponse : BaseResponse, new()
        {
            return new TResponse()
            {
                Errors = _rapidService.GetErrorCodes()
            };
        }

        private CreateTransactionResponse CreateTransaction<TRequest, TResponse>(Func<TRequest, TResponse> invoker, Transaction transaction)
        {
            var request = _mappingService.Map<Transaction, TRequest>(transaction);
            var response = invoker(request);
            return _mappingService.Map<TResponse, CreateTransactionResponse>(response);
        }

        private CreateCustomerResponse CreateCustomer<TRequest, TResponse>(Func<TRequest, TResponse> invoker, Customer customer)
        {
            var request = _mappingService.Map<Customer, TRequest>(customer);
            var response = invoker(request);
            return _mappingService.Map<TResponse, CreateCustomerResponse>(response);
        }

        public QueryTransactionResponse QueryTransaction(TransactionFilter filter)
        {
            if (!filter.IsValid)
            {
                return SdkInternalErrorsResponse<QueryTransactionResponse>();
            }

            var response = new TransactionSearchResponse();
            if (filter.IsValidTransactionID)
            {
                response = _rapidService.QueryTransaction(filter.TransactionID);
            }
            else if (filter.IsValidAccessCode)
            {
                response = _rapidService.QueryTransaction(filter.AccessCode);
            }
            else if (filter.IsValidInvoiceRef)
            {
                response = _rapidService.QueryInvoiceRef(filter.InvoiceReference);
            }
            else if (filter.IsValidInvoiceNum)
            {
                response = _rapidService.QueryInvoiceNumber(filter.InvoiceNumber);
            }

            return _mappingService.Map<TransactionSearchResponse, QueryTransactionResponse>(response);
        }

        public QueryTransactionResponse QueryTransaction(int transactionId)
        {
            return QueryTransaction(Convert.ToInt64(transactionId));
        }

        public QueryTransactionResponse QueryTransaction(long transactionId)
        { 
            var response = _rapidService.QueryTransaction(transactionId);
            return _mappingService.Map<TransactionSearchResponse, QueryTransactionResponse>(response);
        }

        public QueryTransactionResponse QueryTransaction(string accessCode)
        {
            var response = _rapidService.QueryTransaction(accessCode);
            return _mappingService.Map<TransactionSearchResponse, QueryTransactionResponse>(response);
        }
        public QueryTransactionResponse QueryInvoiceNumber(string invoiceNumber)
        {
            var response = _rapidService.QueryInvoiceNumber(invoiceNumber);
            return _mappingService.Map<TransactionSearchResponse, QueryTransactionResponse>(response);
        }
        public QueryTransactionResponse QueryInvoiceRef(string invoiceRef)
        {
            var response = _rapidService.QueryInvoiceRef(invoiceRef);
            return _mappingService.Map<TransactionSearchResponse, QueryTransactionResponse>(response);
        }

        public QueryCustomerResponse QueryCustomer(long tokenCustomerId)
        {
            var request = new DirectCustomerSearchRequest() { TokenCustomerID = tokenCustomerId.ToString() };
            var response = _rapidService.DirectCustomerSearch(request);
            return _mappingService.Map<DirectCustomerSearchResponse, QueryCustomerResponse>(response);
        }

        public RefundResponse Refund(Refund refund)
        {
            var request = _mappingService.Map<Refund, DirectRefundRequest>(refund);
            var response = _rapidService.DirectRefund(request);
            return _mappingService.Map<DirectRefundResponse, RefundResponse>(response);
        }

        public CapturePaymentResponse CapturePayment(CapturePaymentRequest captureRequest)
        {
            var request = _mappingService.Map<CapturePaymentRequest, DirectCapturePaymentRequest>(captureRequest);
            var response = _rapidService.CapturePayment(request);
            return _mappingService.Map<DirectCapturePaymentResponse, CapturePaymentResponse>(response);
        }

        public CancelAuthorisationResponse CancelAuthorisation(CancelAuthorisationRequest cancelRequest)
        {
            var request = _mappingService.Map<CancelAuthorisationRequest, DirectCancelAuthorisationRequest>(cancelRequest);
            var response = _rapidService.CancelAuthorisation(request);
            return _mappingService.Map<DirectCancelAuthorisationResponse, CancelAuthorisationResponse>(response);
        }

        public SettlementSearchResponse SettlementSearch(SettlementSearchRequest settlementSearchRequest)
        {
            if (!IsValid) return SdkInvalidStateErrorsResponse<SettlementSearchResponse>();

            var query = HttpUtility.ParseQueryString(string.Empty);
            var properties = settlementSearchRequest.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(settlementSearchRequest, null);
                if (value != null && !String.IsNullOrWhiteSpace(value.ToString()))
                {
                    if ((!prop.Name.Equals("Page") && !prop.Name.Equals("PageSize")) || !value.Equals(0))
                    {
                        query[prop.Name] = value.ToString();
                    }
                }
            }

            var response = _rapidService.SettlementSearch(query.ToString());
            return _mappingService.Map<DirectSettlementSearchResponse, SettlementSearchResponse>(response);
        }

        public Direct3DSEnrollResponse Direct3DSEnroll(Direct3DSEnrollRequest enrollRequest)
        {
            var request = _mappingService.Map<Direct3DSEnrollRequest, Direct3DSecureEnrollRequest>(enrollRequest);
            var enrollResponse = _rapidService.ThreeDSEnroll(request);
            return _mappingService.Map<Direct3DSecureEnrollResponse, Direct3DSEnrollResponse>(enrollResponse);
        }

        public Direct3DSVerifyResponse Direct3DSVerify(Direct3DSVerifyRequest verifyRequest)
        {
            var request = _mappingService.Map<Direct3DSVerifyRequest, Direct3DSecureVerifyRequest>(verifyRequest);
            var verifyResponse = _rapidService.ThreeDSVerify(request);
            return _mappingService.Map<Direct3DSecureVerifyResponse, Direct3DSVerifyResponse>(verifyResponse);
        }

        public bool IsValid
        {
            get
            {
                return _rapidService.IsValid();
            }
        }

        public string RapidEndpoint
        {
            get { return _rapidService.GetRapidEndpoint(); }
            set { _rapidService.SetRapidEndpoint(value); }
        }
    }
}

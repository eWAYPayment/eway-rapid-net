using System;
using System.Collections.Generic;
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

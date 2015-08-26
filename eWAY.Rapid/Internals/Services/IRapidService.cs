using System.Collections.Generic;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;

namespace eWAY.Rapid.Internals.Services
{
    internal interface IRapidService
    {
        CancelAuthorisationResponse CancelAuthorisation(CancelAuthorisationRequest request);
        CapturePaymentResponse CapturePayment(CapturePaymentRequest request);
        CreateAccessCodeResponse CreateAccessCode(CreateAccessCodeRequest request);
        CreateAccessCodeSharedResponse CreateAccessCodeShared(CreateAccessCodeSharedRequest request);
        GetAccessCodeResultResponse GetAccessCodeResult(GetAccessCodeResultRequest request);
        DirectPaymentResponse DirectPayment(DirectPaymentRequest request);
        DirectAuthorisationResponse DirectAuthorisation(DirectAuthorisationRequest request);
        DirectCustomerResponse DirectCustomerCreate(DirectCustomerRequest request);
        DirectRefundResponse DirectRefund(DirectRefundRequest request);
        DirectCustomerSearchResponse DirectCustomerSearch(DirectCustomerSearchRequest request);
        TransactionSearchResponse QueryTransaction(long transactionID);
        TransactionSearchResponse QueryTransaction(string accessCode);
        TransactionSearchResponse QueryInvoiceRef(string invoiceRef);
        TransactionSearchResponse QueryInvoiceNumber(string invoiceNumber);

        string GetRapidEndpoint();
        void SetRapidEndpoint(string value);
        void SetCredentials(string apiKey, string password);
        bool IsValid();
        List<string> GetErrorCodes();
    }
}
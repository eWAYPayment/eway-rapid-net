using System.Collections.Generic;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;

namespace eWAY.Rapid.Internals.Services
{
    internal interface IRapidService
    {
        DirectCancelAuthorisationResponse CancelAuthorisation(DirectCancelAuthorisationRequest request);
        DirectCapturePaymentResponse CapturePayment(DirectCapturePaymentRequest request);
        CreateAccessCodeResponse CreateAccessCode(CreateAccessCodeRequest request);
        CreateAccessCodeResponse UpdateCustomerCreateAccessCode(CreateAccessCodeRequest request);
        CreateAccessCodeSharedResponse CreateAccessCodeShared(CreateAccessCodeSharedRequest request);
        CreateAccessCodeSharedResponse UpdateCustomerCreateAccessCodeShared(CreateAccessCodeSharedRequest request);
        GetAccessCodeResultResponse GetAccessCodeResult(GetAccessCodeResultRequest request);
        DirectPaymentResponse DirectPayment(DirectPaymentRequest request);
        DirectPaymentResponse UpdateCustomerDirectPayment(DirectPaymentRequest request);
        DirectAuthorisationResponse DirectAuthorisation(DirectAuthorisationRequest request);
        DirectCustomerResponse DirectCustomerCreate(DirectCustomerRequest request);
        DirectRefundResponse DirectRefund(DirectRefundRequest request);
        DirectCustomerSearchResponse DirectCustomerSearch(DirectCustomerSearchRequest request);
        TransactionSearchResponse QueryTransaction(long transactionID);
        TransactionSearchResponse QueryTransaction(string accessCode);
        TransactionSearchResponse QueryInvoiceRef(string invoiceRef);
        TransactionSearchResponse QueryInvoiceNumber(string invoiceNumber);
        DirectSettlementSearchResponse SettlementSearch(string request);
        Direct3DSecureEnrollResponse ThreeDSEnroll(Direct3DSecureEnrollRequest request);
        Direct3DSecureVerifyResponse ThreeDSVerify(Direct3DSecureVerifyRequest request);

        string GetRapidEndpoint();
        void SetRapidEndpoint(string value);
        void SetCredentials(string apiKey, string password);
        void SetVersion(int version);
        bool IsValid();
        List<string> GetErrorCodes();
    }
}
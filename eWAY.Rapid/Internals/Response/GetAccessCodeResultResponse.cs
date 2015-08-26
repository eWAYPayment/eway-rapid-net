using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class GetAccessCodeResultResponse : BaseResponse
    {
        public string AccessCode { get; set; }
        public string AuthorisationCode { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceReference { get; set; }
        public int? TotalAmount { get; set; }
        public int? TransactionID { get; set; }
        public bool? TransactionStatus { get; set; }
        public long? TokenCustomerID { get; set; }
        public decimal? BeagleScore { get; set; }
        public Option[] Options { get; set; }
        public VerificationResult Verification { get; set; }
        public BeagleVerifyResult BeagleVerification { get; set; }
    }
}

using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class DirectPaymentResponse : BaseResponse
    {
        public string AuthorisationCode { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public int? TransactionID { get; set; }
        public bool? TransactionStatus { get; set; }
        public string TransactionType { get; set; }
        public decimal? BeagleScore { get; set; }
        public VerificationResult Verification { get; set; }
        public DirectTokenCustomer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}

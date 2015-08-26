namespace eWAY.Rapid.Models
{
    /// <summary>
    /// This Response is returned by the Refund Method. 
    /// It wraps the TransactionStatus and the echoed back Refund Type with the standard error fields required by an API return type.
    /// </summary>
    public class RefundResponse: BaseResponse
    {
        /// <summary>
        /// The authorisation code for this transaction as returned by the bank
        /// </summary>
        public string AuthorisationCode { get; set; }
        /// <summary>
        /// The two digit response code returned from the bank
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// One or more Response Messages that describes the result of the action performed.
        /// If a Beagle Alert is triggered, this may contain multiple codes: e.g. D4405, F7003
        /// </summary>
        public string ResponseMessage { get; set; }
        /// <summary>
        /// The eWAY transaction ID to search for.
        /// </summary>
        public int? TransactionID { get; set; }
        /// <summary>
        /// This contains the status of the processed refund transaction. 
        /// Any errors that occurred during processing will be reported using the "Errors" member of the RefundResponse.
        /// </summary>
        public bool? TransactionStatus { get; set; }
        /// <summary>
        /// Verification object returned from RefundResponse.
        /// </summary>
        public Verification Verification { get; set; }
        /// <summary>
        /// Contains members that define a Rapid token customer (and card) stored in the Merchant's account.
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// The RefundRequest as echoed back by the Rapid API. 
        /// </summary>
        public RefundDetails Refund { get; set; }
    }
}

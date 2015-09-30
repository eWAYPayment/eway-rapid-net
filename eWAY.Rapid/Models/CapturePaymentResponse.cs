namespace eWAY.Rapid.Models
{
    /// <summary>
    /// The response from eWAY will contain details as to whether the transaction succeeded or failed
    /// </summary>
    public class CapturePaymentResponse: BaseResponse
    {
        /// <summary>
        /// The authorisation code for this transaction as returned by the bank
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// One or more Response Codes that describes the result of the action performed
        /// </summary>
        public string ResponseMessage { get; set; }
        /// <summary>
        /// A unique identifier that represents the transaction in eWAY’s system
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        /// Whether the capture transaction succeeded
        /// </summary>
        public bool TransactionStatus { get; set; }
    }
}

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// This set of fields contains the details of the payment being captured.
    /// </summary>
    public class CapturePaymentRequest
    {
        /// <summary>
        /// Payment detail
        /// </summary>
        public Payment Payment { get; set; }

        /// <summary>
        /// The Transaction ID of the Authorisation to cancel
        /// </summary>
        public string TransactionId { get; set; }
    }
}

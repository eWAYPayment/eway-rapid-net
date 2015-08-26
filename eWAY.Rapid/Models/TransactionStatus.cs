using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Combines together all the status information for a transaction.
    /// </summary>
    public class TransactionStatus
    {
        /// <summary>
        /// The eWAY transaction ID of the transaction. Will only be set once the transaction has been processed or the Authorisation created.
        /// </summary>
        public int TransactionID { get; set; }
        /// <summary>
        /// Total in cents.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// True if the transaction/authorisation was successful
        /// </summary>
        public bool? Status { get; set; }
        /// <summary>
        /// True if the transaction was passed to the bank and funds captured. Depending on the merchant's anti-fraud settings some transactions 
        /// marked as possibly fraudulent may be held for processing until reviewed. In that case this flag will be set to false. 
        /// This flag will also be false if the Transaction's 'Capture' flag was set to false (for an explicit authorisation).
        /// </summary>
        public bool Captured { get; set; }
        /// <summary>
        /// Floating point number indicating the fraud score.
        /// </summary>
        public double BeagleScore { get; set; }
        /// <summary>
        /// When an anti-fraud rule has been triggered this will indicate what action was performed with the transaction.
        /// </summary>
        public FraudAction FraudAction { get; set; }
        /// <summary>
        /// Status of email, address CVN etc Verification. Also contains Beagle Verify results if the transaction was processed through the responsive shared page.
        /// </summary>
        public VerificationResult VerificationResult { get; set; }
        /// <summary>
        /// All the various bank/gateway specific details associated with the transaction.
        /// </summary>
        public ProcessingDetails ProcessingDetails { get; set; }
    }
}

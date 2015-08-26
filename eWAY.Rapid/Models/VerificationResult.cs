using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Contains the result of all the Beagle and Payment provider verification (card scheme/bank)	
    /// </summary>
    public class VerificationResult
    {
        /// <summary>
        /// Result of CVN Verification by card processor
        /// </summary>
        public VerifyStatus CVN { get; set; }
        /// <summary>
        /// Result of Address Verification by card processor
        /// </summary>
        public VerifyStatus Address { get; set; }
        /// <summary>
        /// Result of email verification by card processor
        /// </summary>
        public VerifyStatus Email { get; set; }
        /// <summary>
        /// Result of Mobile verification by card processor
        /// </summary>
        public VerifyStatus Mobile { get; set; }
        /// <summary>
        /// Result of phone verification by card processor
        /// </summary>
        public VerifyStatus Phone { get; set; }
        /// <summary>
        /// Result of email verification from responsive shared page (if processed with a PaymentMethod of ResponsiveShared)
        /// </summary>
        public BeagleVerifyStatus BeagleEmail { get; set; }
        /// <summary>
        /// Result of phone number verification from responsive shared page (if processed with a PaymentMethod of ResponsiveShared)
        /// </summary>
        public BeagleVerifyStatus BeaglePhone { get; set; }
    }
}

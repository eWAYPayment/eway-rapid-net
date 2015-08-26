namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Verification object returned from RefundResponse.
    /// </summary>
    public class Verification
    {
        /// <summary>
        /// Result of CVN Verification by card processor
        /// </summary>
        public string CVN { get; set; }
        /// <summary>
        /// Result of Address Verification by card processor
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Result of email verification by card processor
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Result of Mobile verification by card processor
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Result of phone verification by card processor
        /// </summary>
        public string Phone { get; set; }
    }
}

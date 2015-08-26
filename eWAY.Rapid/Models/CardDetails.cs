namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Card Details
    /// </summary>
    public class CardDetails
    {
        /// <summary>
        /// Name on the card
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Credit card number (16-21 digits plaintext, Up to 512 chars for eCrypted values)
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 2 Digits 
        /// </summary>
        public string ExpiryMonth { get; set; }
        /// <summary>
        /// 2 or 4 digits e.g. "15" or "2015"
        /// </summary>
        public string ExpiryYear { get; set; }
        /// <summary>
        /// 2 digits (required in some countries)
        /// </summary>
        public string StartMonth { get; set; }
        /// <summary>
        /// 2 or 4 digits (required in some countries)
        /// </summary>
        public string StartYear { get; set; }
        /// <summary>
        /// Card issue number (required in some countries)
        /// </summary>
        public string IssueNumber { get; set; }
        /// <summary>
        /// Required for transactions of type Purchase. Optional for other transaction types. (3 or 4 digit number plaintext, up to 512 chars for eCrypted values)
        /// </summary>
        public string CVN { get; set; }
    }
}

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// CreateAccessCodeResponse Payment class
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// The total amount in cents
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// The merchant's invoice number
        /// </summary>
        public string InvoiceNumber { get; set; }
        
        /// <summary>
        /// The merchant's invoice description
        /// </summary>
        public string InvoiceDescription { get; set; }

        /// <summary>
        /// The merchant's invoice reference
        /// </summary>
        public string InvoiceReference { get; set; }

        /// <summary>
        /// The currency for this transaction
        /// </summary>
        public string CurrencyCode { get; set; }
    }
}

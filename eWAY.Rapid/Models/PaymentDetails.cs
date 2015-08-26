namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Payment Details
    /// </summary>
    public class PaymentDetails
    {
        /// <summary>
        /// The total amount to charge the card holder in this transaction in cents. e.g. 1000 = $10.00
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
        /// The currency for this transaction (e.g. AUD)
        /// </summary>
        public string CurrencyCode { get; set; }
    }
}

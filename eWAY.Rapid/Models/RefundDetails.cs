namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Contain the banking details for the refund, including the ID of the tranasaction that is to be refunded (or the auth that is to be cancelled).
    /// </summary>
    public class RefundDetails
    {
        /// <summary>
        /// The ID of either the transaction to refund, or the authorisation to cancel.
        /// </summary>
        public int OriginalTransactionID { get; set; }
        /// <summary>
        /// The total amount to refund the card holder in this transaction in cents. e.g. 1000 = $10.00
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

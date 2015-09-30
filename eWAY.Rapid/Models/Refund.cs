using System.Collections.Generic;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Combines all the high level properties required to process a refund (or Authorisation Cancel)
    /// </summary>
    public class Refund: Payment
    {
        /// <summary>
        /// The eWAY transaction ID to search for.
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        /// Customer details (name, address, token etc)
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// (optional) Shipping Address, name etc for the product ordered with this transaction
        /// </summary>
        public ShippingDetails ShippingDetails { get; set; }
        /// <summary>
        /// Details of the transaction (amount, currency and invoice information)
        /// </summary>
        public RefundDetails RefundDetails { get; set; }
        /// <summary>
        /// (optional) Invoice Line Items for the purchase
        /// </summary>
        public List<LineItem> LineItems { get; set; }
        /// <summary>
        /// (optional) General Options for the transaction
        /// </summary>
        public List<string> Options { get; set; }
        /// <summary>
        /// (optional) Used to supply an identifier for the device sending the transaction.
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// (optional) Used by shopping carts/ partners.
        /// </summary>
        public string PartnerID { get; set; }
        /// <summary>
        /// The customer’s IP address
        /// </summary>
        public string CustomerIP { get; set; }
    }
}

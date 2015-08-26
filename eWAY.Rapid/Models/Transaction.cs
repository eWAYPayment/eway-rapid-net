using System.Collections.Generic;
using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// The details of a transaction that will be processed either via the responsive shared page, 
    /// by transparent redirect, by Direct, or one that is captured from a previous Authorisation transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// What type of transaction this is (Purchase, MOTO,etc)
        /// </summary>
        public TransactionTypes TransactionType { get; set; }
        /// <summary>
        /// Set to true to create a regular transaction with immediate capture (default).
        /// Set to false to create an Authorisation transaction that can be used in a subsequent transaction.
        /// </summary>
        public bool Capture { get; set; }
        /// <summary>
        /// Customer details (name address token etc)
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// (optional) Shipping Address, name etc for the product ordered with this transaction
        /// </summary>
        public ShippingDetails ShippingDetails { get; set; }
        /// <summary>
        /// Payment details (amount, currency and invoice information)
        /// </summary>
        public PaymentDetails PaymentDetails { get; set; }
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
        /// (optional) Used when a Third Party Digital wallet will be supplying the Card Details.
        /// </summary>
        public string ThirdPartyWalletID { get; set; }
        /// <summary>
        /// (optional) Used with a PaymentType of Authorisation. This specifies the original authorisation that the funds are to be captured from.
        /// </summary>
        public int AuthTransactionID { get; set; }
        /// <summary>
        /// (optional) Used by transactions with a CardSource of TransparentRedirect, or ResponsiveShared This field specifies the URL on the 
        /// merchant's site that the RapidAPI will redirect the cardholder's browser to after processing the transaction.
        /// </summary>
        public string RedirectURL { get; set; }
        /// <summary>
        /// (optional) Used by transactions with a card source of ResponsiveShared. This field specifies the URL on the merchant's 
        /// site that the responsive page redirect the cardholder to if they choose to cancel the transaction.
        /// </summary>
        public string CancelURL { get; set; }
        /// <summary>
        /// (optional) The URL used for the integrating PayPal Checkout
        /// </summary>
        public string CheckoutURL { get; set; }
        /// <summary>
        /// (optional) Flag to set if the PayPal Checkout should be used for this
        /// </summary>
        public bool CheckoutPayment { get; set; }
        /// <summary>
        /// The customer’s IP address
        /// </summary>
        public string CustomerIP { get; set; }

        public Transaction()
        {
            // Default to capture
            Capture = true;
        }
    }
}

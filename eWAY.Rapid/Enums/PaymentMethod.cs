namespace eWAY.Rapid.Enums
{
    /// <summary>
    /// This defines what method will be used by the transaction to determine the credit card used to process the transaction
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// The Card is supplied in the Transaction Request itself (in the Customer.CardDetails member, or from the Customer's card in the eWay vault). 
        /// If supplied from the request, the Card Data can either be plaintext (for PCI Compliant Customers) or encrypted using the client-side eCrypt 
        /// javascript library. For this Paymentmethod, the transaction/authorisation is processed immediately.
        /// </summary>
        Direct,
        /// <summary>
        /// The Card will be supplied by redirecting the cardholder to the Responsive Shared PageeWay. For this PaymentMethod, the transaction/authorisation 
        /// is processed once the cardholder submits the responsive shared page.
        /// </summary>
        ResponsiveShared,
        /// <summary>
        /// The Card will supplied by a form post from a Transparent Redirect page. For this PaymentMethod, the transaction/authorisation is processed 
        /// when the form is posted.
        /// </summary>
        TransparentRedirect,
        /// <summary>
        /// The Card will be obtained from a digital wallet (e.g. Visa Checkout).
        /// </summary>
        Wallet,
        /// <summary>
        /// A previously created authorisation will be used to create the transaction by using an Auth Capture.
        /// </summary>
        Authorisation
    }
}

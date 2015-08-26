namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Contains members that define a Rapid token customer (and card) stored in the merchant's account.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// eWAY Token ID identifying this customer
        /// </summary>
        public string TokenCustomerID { get; set; }
        /// <summary>
        /// Merchant's own reference ID for the customer
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// Customer's title
        /// One of: "Mr.", "Ms.", "Mrs.", "Miss", "Dr.", "Sir.", "Prof."
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Customer's First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Customer's Last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Customer's company name
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Role or job description
        /// </summary>
        public string JobDescription { get; set; }
        /// <summary>
        /// Customer's address
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// Customer's Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Customer's Mobile Phone
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Customer's Fax number
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// URL for customer's site
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Comments attached to this customer.
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// If the customer is active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// (optional) Used by transactions with a CardSource of TransparentRedirect, or ResponsiveShared This field specifies the URL on the 
        /// merchant's site that the RapidAPI will redirect the cardholder's browser to after processing the transaction.
        /// </summary>
        public string RedirectURL { get; set; }
        /// <summary>
        /// Used for Direct PaymentMethods The card details for this customer.
        /// </summary>
        public CardDetails CardDetails { get; set; }
        /// <summary>
        /// The customer’s IP address
        /// </summary>
        public string CustomerIP { get; set; }
    }
}

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// The response is returned from a CreateTransaction method call. It will echo back the details of the Customer that has been, or will be, created.
    /// </summary>
    public class CreateCustomerResponse : BaseResponse
    {
        /// <summary>
        /// The Customer created by the method call. This will echo back the properties of the Customer adding the TokenCustomerID for the created customer. 
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// (Only for payment method of ResponsiveShared) URL to the Responsive Shared Page that the cardholder's browser should be redirected to 
        /// to capture the card to save with the new customer.
        /// </summary>
        public string SharedPaymentUrl { get; set; }
        /// <summary>
        /// (Only for payment method of TransparentRedirect) URL That the merchant's credit card collection form should post to to capture 
        /// the card to be saved with the new customer.
        /// </summary>
        public string FormActionUrl { get; set; }
        /// <summary>
        /// The AccessCode for this transaction (can be used with the customer query method call for searching before and after the card capture is completed)
        /// </summary>
        public string AccessCode { get; set; }
    }
}

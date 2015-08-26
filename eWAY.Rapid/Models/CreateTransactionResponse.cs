namespace eWAY.Rapid.Models
{
    /// <summary>
    /// The response is returned from a CreateTransaction method call. This will echo back the details of the Transaction (Customer, Payment, Items, options etc). 
    /// </summary>
    public class CreateTransactionResponse : BaseResponse
    {
        /// <summary>
        /// The Transaction as echoed back by the Rapid API. Where a token customer is created as result of the transaction, then the Customer object in the RequestDetails will contain the token ID.
        /// </summary>
        public Transaction Transaction { get; set; }
        /// <summary>
        /// (Only for Direct payment methods) This contains the status of the processed transaction. 
        /// Any errors that occurred while processing will be reported using the "Errors" member of the CreateTransactionResponse.
        /// </summary>
        public TransactionStatus TransactionStatus { get; set; }
        /// <summary>
        /// (Only for payment methods of ResponsiveShared) URL to the Responsive Shared Page that the cardholder's browser should be redirected to to complete payment
        /// </summary>
        public string SharedPaymentUrl { get; set; }
        /// <summary>
        /// (Only for payment methods of TransparentRedirect) URL That the merchant's credit card collection form should post to to complete payment.
        /// </summary>
        public string FormActionUrl { get; set; }
        /// <summary>
        /// The AccessCode for this transaction (can be used to call query transaction for searching before the transaction has completed processing)
        /// </summary>
        public string AccessCode { get; set; }
    }
}

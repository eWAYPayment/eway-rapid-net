namespace eWAY.Rapid.Models
{
    /// <summary>
    /// This response simply wraps the TransactionStatus type with the additional common fields required by a return type.
    /// </summary>
    public class QueryTransactionResponse: BaseResponse
    {
        /// <summary>
        /// The Request as echoed back by the Rapid API. Where a token customer is created as result of the transaction, 
        /// then the Customer member in this type will contain the token ID.
        /// </summary>
        public Transaction Transaction { get; set; }
        /// <summary>
        /// This contains the status of the processed transaction. Any errors that occurred during processing will be 
        /// reported using the "Errors" member of the QueryTransactionResponse.
        /// </summary>
        public TransactionStatus TransactionStatus { get; set; }
        /// <summary>
        /// The access code for this transaction. 
        /// </summary>
        public string AccessCode { get; set; }
    }
}

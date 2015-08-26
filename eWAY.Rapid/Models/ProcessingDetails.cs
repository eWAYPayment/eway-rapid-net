namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Combines together all the bank/gateway specific status information for a transaction
    /// </summary>
    public class ProcessingDetails
    {
        /// <summary>
        /// The Bank Auth code for the transaction
        /// </summary>
        public string AuthorisationCode { get; set; }
        /// <summary>
        /// The bank/gateway Response code
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// The bank/gateway response message
        /// </summary>
        public string ResponseMessage { get; set; }
    }
}

namespace eWAY.Rapid.Internals.Models
{
    internal class TransactionResult
    {
        public string AuthorisationCode { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceReference { get; set; }
        public int? TotalAmount { get; set; }
        public int? TransactionID { get; set; }
        public bool? TransactionStatus { get; set; }
        public long? TokenCustomerID { get; set; }
        public decimal? BeagleScore { get; set; }
        public Option[] Options { get; set; }
        public VerificationResult Verification { get; set; }
        public BeagleVerifyResult BeagleVerification { get; set; }
        public Customer Customer { get; set; }
        public string CustomerNote { get; set; }
        public ShippingAddress ShippingAddress { get; set; }

        //Rapid v40 fields
        public string TransactionDateTime { get; set; }
        public string FraudAction { get; set; }
        public bool? TransactionCaptured { get; set; }
        public string CurrencyCode { get; set; }
        public int? Source { get; set; }
        public int? MaxRefund { get; set; }
        public int? OriginalTransactionId { get; set; }
}
}

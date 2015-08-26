namespace eWAY.Rapid.Internals.Models
{
    /// <summary>
    /// CreateAccessCodeResponse Payment class
    /// </summary>
    internal class Payment
    {   
        public int TotalAmount { get;set; }
        public string InvoiceNumber { get;set; }
        public string InvoiceDescription { get;set; }
        public string InvoiceReference { get;set; }
        public string CurrencyCode { get; set; }
    }
}

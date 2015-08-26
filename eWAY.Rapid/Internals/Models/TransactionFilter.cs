using eWAY.Rapid.Internals.Enums;

namespace eWAY.Rapid.Internals.Models
{
    internal class TransactionFilter
    {
        public FilterMatchType TransactionIDMatchType { get;set; }
        public string TransactionID  { get;set; }
        public FilterMatchType AccessCodeMatchType  { get;set; }
        public string AccessCode  { get;set; }
        public FilterMatchType CustomerIDMatchType  { get;set; }
        public string CustomerID  { get;set; }
        public FilterMatchType InvoiceReferenceMatchType  { get;set; }
        public string InvoiceReference  { get;set; }
        public FilterMatchType InvoiceNumberMatchType  { get;set; }
        public string InvoiceNumber { get; set; }
    }
}

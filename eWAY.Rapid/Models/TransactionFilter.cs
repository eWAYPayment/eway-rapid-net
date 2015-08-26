using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Defines a set of parameters used to search for a transaction. This is defined as per the Rapid Transaction Filter. 
    /// Only one of the properties should be provided.
    /// </summary>
    public class TransactionFilter
    {
        /// <summary>
        /// Match Transaction ID.
        /// </summary>
        public FilterMatchType TransactionIDMatchType { get; set; }
        /// <summary>
        /// The eWAY transaction ID to search for.
        /// </summary>
        public int TransactionID { get; set; }
        /// <summary>
        /// Match Access code.
        /// </summary>
        public FilterMatchType AccessCodeMatchType { get; set; }
        /// <summary>
        /// The access code to search for.
        /// </summary>
        public string AccessCode { get; set; }
        /// <summary>
        /// Match Invoice reference.
        /// </summary>
        public FilterMatchType InvoiceReferenceMatchType { get; set; }
        /// <summary>
        /// The Invoice reference to search for (merchant supplied). Must be unique to return a transaction.
        /// </summary>
        public string InvoiceReference { get; set; }
        /// <summary>
        /// Match invoice number.
        /// </summary>
        public FilterMatchType InvoiceNumberMatchType { get; set; }
        /// <summary>
        /// The Invoice number to search for (merchant supplied). Must be unique to return a transaction
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Returns true if this transaction filter is for a Transaction ID
        /// </summary>
        public bool IsValidTransactionID
        {
            get
            {
                return (TransactionID > 0) && (AccessCode == null) && (InvoiceReference == null) && (InvoiceNumber == null);
            }
        }

        /// <summary>
        /// Returns true if this transaction filter is for an Access Code
        /// </summary>
        public bool IsValidAccessCode
        {
            get
            {
                return (TransactionID == 0) && (AccessCode != null) && (InvoiceReference == null) && (InvoiceNumber == null);
            }
        }

        /// <summary>
        /// Returns true if this transaction filter is for an Invoice Reference
        /// </summary>
        public bool IsValidInvoiceRef
        {
            get
            {
                return (TransactionID == 0) && (AccessCode == null) && (InvoiceReference != null) && (InvoiceNumber == null);
            }
        }

        /// <summary>
        /// Returns true if this transaction filter is for an Invoice Numebr
        /// </summary>
        public bool IsValidInvoiceNum
        {
            get
            {
                return (TransactionID == 0) && (AccessCode == null) && (InvoiceReference == null) && (InvoiceNumber != null);
            }
        }

        /// <summary>
        /// Returns true if this transaction filter has a search filter set
        /// </summary>
        public bool IsValid 
        { 
            get 
            { 
                return IsValidTransactionID || IsValidInvoiceRef || IsValidInvoiceNum || IsValidAccessCode; 
            } 
        }
    }
}
